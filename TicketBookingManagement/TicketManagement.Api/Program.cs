using TicketManagement.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices().ConfigurePipelines();
await app.ResetDatabaseAsync();
app.Run();