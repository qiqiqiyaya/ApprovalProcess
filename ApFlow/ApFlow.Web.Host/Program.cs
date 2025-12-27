using ApFlow.Web.Host;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("Logs/logs.txt"))
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac()
    .UseSerilog();

builder.Services.AddApplication<ApFlowHostModule>();

var app = builder.Build();

await app.InitializeApplicationAsync();

app.Run();