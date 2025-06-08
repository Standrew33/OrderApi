using OrderApi.DTOs;
using System.Collections.Concurrent;

namespace OrderApi.Services
{
    public class PaymentQueueService
    {
        private readonly ConcurrentQueue<PaymentRequestDto> _queue = new();
        private readonly IServiceScopeFactory _scopeFactory;

        public PaymentQueueService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Enqueue(PaymentRequestDto payment)
        {
            _queue.Enqueue(payment);
        }

        public async Task ProcessQueueAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var payment))
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<Data.OrderDbContext>();
                    var order = db.Orders.FirstOrDefault(o => o.OrderNumber == payment.OrderNumber);

                    if (order != null)
                    {
                        order.Status = payment.IsPaid ? Models.OrderStatus.Paid : Models.OrderStatus.Cancel;
                        await db.SaveChangesAsync();
                    }
                }

                await Task.Delay(500);
            }
        }
    }
}
