using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Back_End.Entities;
using Back_End.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Back_End.Validator;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(setupAction =>
        {
            setupAction.ReturnHttpNotAcceptable = true;
        })
          .AddFluentValidation(fv => {
              fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
              fv.RegisterValidatorsFromAssemblyContaining<Startup>();
          })

        .AddNewtonsoftJson(setupAction =>
        {
            setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        })
        .AddXmlDataContractSerializerFormatters();
      

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //Defino los repositorios a usar
        services.AddScoped<ICruzRojaRepository<Employees>, EmployeesRepository>();

        services.AddScoped<ICruzRojaRepository<Volunteers>, VolunteersRepository>();

        services.AddScoped<ICruzRojaRepository<Users>, UsersRepository>();


        //Defino la conexion con la base de datos
        var connection = Configuration.GetConnectionString("CruzRojaDB");
        services.AddDbContextPool<CruzRojaContext2>(options => options.UseSqlServer(connection));
        services.AddControllers();

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

        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseRouting();



        app.UseAuthentication();

        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.UseMvc();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
           
    }   
}
