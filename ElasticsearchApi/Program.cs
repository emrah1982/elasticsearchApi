using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ElasticsearchApi;

var builder = WebApplication.CreateBuilder(args);

// Create startup instance using the configuration and environment
var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app);

app.Run();
