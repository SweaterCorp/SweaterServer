using CommonLibraries;
using CommonLibraries.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZebraData;
using ZebraData.Repositories;
using ZebraMain.Infrastructure;

namespace ZebraMain
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
      });
      services.AddDbContext<ZebraMainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ZebraMainConnection")));
      //services.Configure<ConnectionStrings>(options => options.ZebraConnection = Configuration.GetConnectionString("ZebraMainConnection"));
      services.AddTransient<ProductRepository>();
      services.AddTransient<UserRepository>();
      services.AddTransient<MediaService>();
      services.AddOptions();
      services.Configure<ServersSettings>(Configuration.GetSection("ServersSettings"));
      services.Configure<MediaSettings>(Configuration.GetSection("MediaFolder"));

      //var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtSettings));
      //var secretKey = jwtAppSettingOptions["SecretKey"];
      //var issuer = jwtAppSettingOptions[nameof(JwtSettings.Issuer)];
      //var audience = jwtAppSettingOptions[nameof(JwtSettings.Audience)];

      //services.Configure<JwtSettings>(options =>
      //{
      //  options.Issuer = issuer;
      //  options.Audience = audience;
      //  options.SigningCredentials = new SigningCredentials(JwtSettings.CreateSecurityKey(secretKey),
      //    SecurityAlgorithms.HmacSha256);
      //});

      //services.AddAuthentication(options =>
      //{
      //  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      //  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      //}).AddJwtBearer(configureOptions =>
      //{
      //  configureOptions.ClaimsIssuer = issuer;
      //  configureOptions.RequireHttpsMetadata = false;
      //  configureOptions.TokenValidationParameters =
      //    JwtSettings.CreateTokenValidationParameters(issuer, audience, JwtSettings.CreateSecurityKey(secretKey));
      //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment()) loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();
      app.UseExceptionHandling();

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });

      app.UseAuthentication();
      app.UseMvc();
    }
  }
}