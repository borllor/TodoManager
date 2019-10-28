using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoManager.Bus;
using TodoManager.Dal;
using TodoManager.Dal.Cache;
using TodoManager.Domain.Commands;
using TodoManager.Domain.Events;
using TodoManager.Domain.Handlers;
using TodoManager.Domain.Services;
using TodoManager.Filter;
using TodoManager.Framework.Events;
using TodoManager.Framework.Handlers;
using TodoManager.Framework.Querys;
using TodoManager.Models;

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

            //config dbContext
            ConfigDbContext(services);

            ConfigureCQRS(services);
            ConfigCache(services);
            ConfigMvc(services);
            //config cors
            ConfigCors(services);

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

        private static void ConfigureCQRS(IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            services.AddScoped<IRequestHandler<CreateTodoItemCommand, Unit>, TodoItemCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTodoItemCommand, Unit>, TodoItemCommandHandler>();
            services.AddScoped<IRequestHandler<ChangeStateOfTodoItemCommand, Unit>, TodoItemCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteTodoItemCommand, Unit>, TodoItemCommandHandler>();

            services.AddScoped<IRequestHandler<GetTodoItemQuery, TodoItemDto>, TodoItemQueryHandler>();
            services.AddScoped<IRequestHandler<GetTodoItemsQuery, IEnumerable<TodoItemDto>>, TodoItemQueryHandler>();

            services.AddScoped<INotificationHandler<TodoItemEvent>, TodoItemEventHandler>();
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

        private void ConfigCache(IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration.GetConnectionString("RedisConnection");
                option.InstanceName = "master";
            });
            services.AddSingleton<IDisctributedCacheProvider, RedisProdiver>();

        }

        private void ConfigMvc(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });
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
