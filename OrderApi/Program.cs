using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlite("Data Source=orders.db"));

builder.Services.AddSingleton<PaymentQueueService>();
builder.Services.AddHostedService<PaymentBackgroundService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
