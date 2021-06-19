using System.Net;
using Basket.API.Database.Helpers.Collections;
using Basket.API.Database.Helpers.Collections.Interfaces;
using Basket.API.Database.Repositories;
using Basket.API.Database.Repositories.Interfaces;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace Basket.API
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
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddSingleton<IBasketContext, BasketContext>();
            services.AddSingleton<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                    factory.UserName = Configuration["EventBus:UserName"];
                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                    factory.Password = Configuration["EventBus:Password"];
                return new RabbitMQConnection(factory);
            });
            services.AddSingleton<EventBusRabbitMQProducer>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Basket.API", Version = "v1"});
            });
            // services.AddHttpsRedirection(options =>
            // {
            //     options.RedirectStatusCode = (int) HttpStatusCode.TemporaryRedirect;
            //     options.HttpsPort = 8001;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}