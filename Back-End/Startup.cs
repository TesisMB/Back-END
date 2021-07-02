using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Back_End.Validator;
using FluentValidation;
using Back_End.Models;
using Back_End;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using System.IO;
using FluentValidation.AspNetCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.ConfigureCors();
        services.ConfigureIISIntegreation();
        services.ConfigureLoggerService();
        services.ConfigureSqlContext(Configuration);
        services.ConfigureRepositoryWrapper();
        services.Fluent();


        services.Configure<ApiBehaviorOptions>(options => {
           options.SuppressModelStateInvalidFilter = true;
        });

        services.AddControllers();

        services.AddControllers(setupAction =>
        {
            setupAction.ReturnHttpNotAcceptable = true;
        })


        .AddNewtonsoftJson(setupAction =>
        {
            setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        })
        .AddXmlDataContractSerializerFormatters();


        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })

     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = "http://localhost:5000",
             ValidAudience = "http://localhost:5000",
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
         };
     });

        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        services.AddMvc(Options => Options.EnableEndpointRouting = false);

     
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //Entornoo de desarollo
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected fault happened. Try again later. ");
                });
            });
        }

        app.UseHttpsRedirection();

        //habilita e uso de archivos estaticos para la solicitud.
        app.UseStaticFiles();

        //Reenvia los encabezados del proxy a la solicitud actual.
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseMvc();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}
