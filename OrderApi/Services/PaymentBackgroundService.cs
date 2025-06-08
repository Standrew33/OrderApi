namespace OrderApi.Services
{
    public class PaymentBackgroundService : BackgroundService
    {
        private readonly PaymentQueueService _queue;

        public PaymentBackgroundService(PaymentQueueService queue)
        {
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _queue.ProcessQueueAsync(stoppingToken);
        }
    }
}
