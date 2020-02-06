using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceManual.Config;

namespace ServiceManual
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure Swagger
            services.AddSwaggerGen(x => { x.SwaggerDoc("v1", new OpenApiInfo { Title="Service Manual API", Version="v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configure DatabaseConnections
            DatabaseConfig dbConfig = new DatabaseConfig();
            Configuration.GetSection(nameof(DatabaseConfig)).Bind(dbConfig);
            Database.Config = dbConfig;

            // Configure Swagger
            SwaggerOptions swagger = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swagger);
            app.UseSwagger(option => { option.RouteTemplate = swagger.JsonRoute; });
            app.UseSwaggerUI(option => { option.SwaggerEndpoint(swagger.UiEndpoint, swagger.Description); });
        }
    }
}
