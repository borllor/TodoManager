using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoManager.Dal;
using TodoManager.Domain.Services;

namespace TodoManager
{
    public class Startup
    {
        private const String RUSH_CORS_POLICY_NAME = "TodoItemCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMediator(services);
            //config cors
            ConfigCors(services);

            //config dbContext
            ConfigDbContext(services);

            //config services
            ConfigServices(services);

            services.AddControllers();
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
        }

        private static void ConfigureMediator(IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
        }

        private void ConfigCors(IServiceCollection services)
        {
            services.AddCors(cors =>
            {
                cors.AddPolicy(RUSH_CORS_POLICY_NAME, p =>
                {
                    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        private void ConfigServices(IServiceCollection services)
        {
            services.AddSingleton<LoginService>();
        }

        private void ConfigDbContext(IServiceCollection services)
        {
            services.AddDbContext<TodoItemContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TodoItemContext"));
            });
        }

    }
}
