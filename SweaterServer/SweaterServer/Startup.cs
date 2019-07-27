using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonLibraries;
using CommonLibraries.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductDatabase;
using ProductDatabase.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SweaterServer
{
  /// <summary>
  ///   The startup.
  /// </summary>
  public class Startup
  {
    /// <summary>
    ///   Gets the configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    public ILogger<Startup> Logger { get; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Startup" /> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="config">
    ///   The config.
    /// </param>
    public Startup(ILogger<Startup> logger, IConfiguration config)
    {
      Configuration = config;
      Logger = logger;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddLocalization(options => options.ResourcesPath = "Resources");

      services.AddCors(options =>
      {
        options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
      });

      Logger.LogInformation($"{nameof(Startup)}.{nameof(ConfigureServices)}: Add repositories begin.");
      services.AddDbContext<ProductContext>(
        options => options.UseSqlServer(Configuration.GetConnectionString("SweaterMainConnection")));
      services.AddTransient<QueriesRepository>();
      services.AddTransient<ColorGoodnessRepository>();
      services.AddTransient<ProductRepository>();
      services.AddTransient<ProductColorGoodnessRepository>();
      services.AddTransient<UserRepository>();
      services.AddTransient<MonitoringRepository>();
      Logger.LogInformation($"{nameof(Startup)}.{nameof(ConfigureServices)}: Add repositories end.");

      services.AddOptions();

      services.Configure<ServersSettings>(Configuration.GetSection("ServersSettings"));

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.CustomSchemaIds(DefaultSchemaIdSelector);
        c.SwaggerDoc("v1",
          new Info {Title = "Sweater App Server", Version = "v1", Description = "API for Sweater App server"});
        //c.AddSecurityDefinition("Bearer",
        //  new ApiKeyScheme
        //  {
        //    Description = "JWT Authorization header using the Bearer scheme. Example: Bearer {token}",
        //    Name = "Authorization",
        //    In = "header",
        //    Type = "apiKey"
        //  });
        //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } } });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });

      return services.BuildServiceProvider();
    }

    /// <summary>
    ///   The configure.
    ///   This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">
    ///   The app.
    /// </param>
    public void Configure(IApplicationBuilder app)
    {
      app.UseExceptionHandling();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "1C Support V1");
        c.RoutePrefix = "swagger";
      });

      var supportedCultures = new[] {new CultureInfo("en"), new CultureInfo("ru")};
      app.UseRequestLocalization(new RequestLocalizationOptions
      {
        DefaultRequestCulture = new RequestCulture("ru"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
      });

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });

      app.UseRequestLocalization();
      app.UseAuthentication();
      app.UseMvc();
    }

    private static string DefaultSchemaIdSelector(Type modelType)
    {
      if (!modelType.IsConstructedGenericType) return modelType.Name;

      var prefix = modelType.GetGenericArguments().Select(DefaultSchemaIdSelector)
        .Aggregate((previous, current) => previous + current);

      return prefix + modelType.Name.Split('`').First();
    }
  }
}