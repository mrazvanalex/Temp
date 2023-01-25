using Pos.PeripheralManager.Config;
using Pos.PeripheralManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggingConfig = builder.Configuration.GetSection("VirtualCashDrawerConfig:ManagerConfig").Get<ManagerConfig>();

builder.Services.AddSingleton<ICashDrawerService, CashDrawerService>();
builder.Services.AddHostedService<HostedService>();
builder.Services.AddSingleton<IManagerConfig>(loggingConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
