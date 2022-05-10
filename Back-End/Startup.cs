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
using Back_End;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using System.IO;
using Wkhtmltopdf.NetCore;
using Back_End.Hubs;
using Microsoft.Extensions.FileProviders;
using DinkToPdf.Contracts;
using DinkToPdf;

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

        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


        services.ConfigureCors();
        services.ConfigureIISIntegreation();
        services.ConfigureLoggerService();
        services.ConfigureSqlContext(Configuration);
        services.ConfigureRepositoryWrapper();
        services.Fluent();
        services.AddWkhtmltopdf("WkhtmltoPdf");

        services.AddCors(options =>
        {
            options.AddPolicy("todos",
                builder => {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    //dominios
                    .SetIsOriginAllowed((Host) => true)
                    .AllowCredentials();
                });
        });

        services.AddSignalR();

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


        var configuration = new MapperConfiguration(cfg => {
            cfg.AllowNullCollections = true;
        });


        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
  {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("superSecretKey@345")),
      ValidateIssuer = false,
      ValidateAudience = false,
      ClockSkew = TimeSpan.Zero
      //ValidateLifetime = true,

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



        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
            RequestPath = new PathString("/StaticFiles")
        });


        //habilita e uso de archivos estaticos para la solicitud.
        /*app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "StaticFiles", "Images")),
            RequestPath = "/StaticFiles/Images"
        });*/

        //Reenvia los encabezados del proxy a la solicitud actual.
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseCors("todos");

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.UseMvc();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            //endpoints.MapHub<Mensaje>("Notifications");
            endpoints.MapHub<Mensaje>("/chat");
        });

    }
}
