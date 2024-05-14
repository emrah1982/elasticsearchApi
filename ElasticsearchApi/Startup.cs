using Nest;
using ElasticsearchApi.Context.MySQL;
using ElasticsearchApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static ElasticsearchApi.Services.IElasticsearchService;
using Microsoft.OpenApi.Models;
using ElasticsearchApi.Filter;
using ElasticsearchApi.Core;
using ElasticsearchApi.Model;

namespace ElasticsearchApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddConsole();
                config.AddDebug();
                // Diğer logging sağlayıcıları...
            });
            //var logger = services.BuildServiceProvider().GetService<ILogger<Startup>>();

            //logger.LogInformation("Registering Elasticsearch client.");
            //// Elasticsearch client registration
            //logger.LogInformation("Elasticsearch client registered.");

            // Configuration for Elasticsearch client
            var settings = new ConnectionSettings(new Uri(Configuration["ElasticsearchSettings:Uri"]))
                           .DefaultIndex(Configuration["ElasticsearchSettings:IndexName"]);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);


            services.AddSingleton(typeof(IElasticsearchService<>), typeof(ElasticsearchService<>));
            services.AddScoped(typeof(DatabaseService<>));

            //services.AddScoped<IMySQLService, MySQLService>();

            // MVC ve Swagger yapılandırmaları
            services.AddControllers().AddApplicationPart(typeof(GenericBaseController<>).Assembly);
            //services.AddMvc();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElasticSearch API  ", Version = "v1" });
                //c.DocumentFilter<GenericControllerDocumentFilter>(); // Register your custom document filter
                c.DocumentFilter<GenericControllerSwaggerConfig>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            // Uygulamanın nasıl yapılandırılacağı
            if (_environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseExceptionHandler();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}
