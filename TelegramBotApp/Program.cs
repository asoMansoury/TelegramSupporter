using TeelgramBotSupporter.TelegramServices;
using TelegramBotApp;
using TelegramBotApp.TelegramServices.ManagingServices;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

ITelegramBotService telegramBotService = new TelegramBotService(ConfigData.TelegramToken);
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
