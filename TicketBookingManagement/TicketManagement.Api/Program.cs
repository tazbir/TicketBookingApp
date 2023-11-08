using TicketManagement.Api;

using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("API application is starting");


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

var app = builder.ConfigureServices().ConfigurePipelines();

app.UseSerilogRequestLogging();
//await app.ResetDatabaseAsync();
app.Run();

public partial class Program
{
}