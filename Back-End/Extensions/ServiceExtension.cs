using Back_End.Entities;
using Contracts.Interfaces;
using FluentValidation.AspNetCore;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;

namespace Back_End
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin() //Permite solicitudes de cualquier fuente
                    .AllowAnyMethod() // Permite que se puedan usar todos los metodos HTTP
                    .AllowAnyHeader()); //Permite todos los encabezados
            });

        }
            public static void ConfigureIISIntegreation(this IServiceCollection services)
            {
                //Valores por default
               services.Configure<IISOptions>(options =>
               {

               });
            }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:CruzRojaDB"];
            services.AddDbContext<CruzRojaContext>(a => a.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositorWrapper, RepositoryWrapper>();
        }
        
        public static void Fluent (this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
    }
}

