using System;
using System.IO;
using CommonLibraries.CommandLine;
using CommonLibraries.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;

namespace SweaterServer
{
  /// <summary>
  ///   The program.
  /// </summary>
  public class Program
  {
    public static HostCommand HostCommand { get; private set; } = new HostCommand();
    public static string Scheme { get; } = "http";
    public static string IpAddress { get; } = "localhost";
    public static string Port { get; private set; }
    public static string Url => Scheme + "://" + IpAddress + ":" + Port;

    /// <summary>
    ///   The create web host builder.
    /// </summary>
    /// <param name="args">
    ///   The args.
    /// </param>
    /// <returns>
    ///   The <see cref="IWebHostBuilder" />.
    /// </returns>
    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory);

      builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      builder.AddJsonFile("hiddensettings.json", optional: false, reloadOnChange: true);

      var configuration = builder.Build();
      Port = configuration["ServerSettings:Port"];

      if (!HostCommand.Port.IsNullOrEmpty()) Port = HostCommand.Port;


      return WebHost.CreateDefaultBuilder(args).UseConfiguration(configuration).UseUrls(Url)
        .ConfigureLogging((hostingContext, logging) =>
        {
          logging.ClearProviders();
          logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
          logging.AddConsole();
          logging.AddDebug();
          logging.AddEventSourceLogger();
        }).UseSerilog(ConfigureLogger).UseStartup<Startup>();
    }

    /// <summary>
    ///   The main.
    /// </summary>
    /// <param name="args">
    ///   The args.
    /// </param>
    public static void Main(string[] args)
    {
      var commandLine = Command.WithName("host").HasOption("-port", "-p").HasOption("-swagger", "-swg")
        .HasOption("-currentdirectory", "-curdir");
      ;

      if (commandLine.TryParse(args, out var command) && command is HostCommand hostCommand) HostCommand = hostCommand;

      var build = CreateWebHostBuilder(args).Build();
     // var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      if (!HostCommand.CurrentDirectory.IsNullOrEmpty())
      {
        var dir = Path.GetDirectoryName(HostCommand.CurrentDirectory);
        Directory.SetCurrentDirectory(dir);
      }

     // if (!HostCommand.Swagger.IsNullOrEmpty()) logger.Debug(GenerateSwagger(build, args[1]));

      try
      {
        build.Run();
      }
      catch (Exception ex)
      {
      // logger.Error(ex, "Stopped program because of exception");
        throw;
      }
      finally
      {
        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //LogManager.Shutdown();
      }
    }

    /// <summary>
    ///   The generate swagger.
    /// </summary>
    /// <param name="host">
    ///   The host.
    /// </param>
    /// <param name="docName">
    ///   The doc name.
    /// </param>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    private static string GenerateSwagger(IWebHost host, string docName)
    {
      var sw = (ISwaggerProvider)host.Services.GetService(typeof(ISwaggerProvider));
      var doc = sw.GetSwagger(docName, null, "/");

      using (var writer = new StringWriter())
      {
        writer.Write(JsonConvert.SerializeObject(doc, Formatting.Indented,
          new JsonSerializerSettings
          {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new SwaggerContractResolver(new JsonSerializerSettings())
          }));

        var file = AppDomain.CurrentDomain.BaseDirectory + "swagger.json";
        using (var stream = new StreamWriter(file))
        {
          stream.WriteLine(writer.ToString());
        }
      }

      return "Swagger was created";
    }
  

  private static void ConfigureLogger(WebHostBuilderContext ctx, LoggerConfiguration cfg)
    {
      var serverName = "Sweater Server";
      var path = Path.Combine(AppContext.BaseDirectory, ".." + Path.DirectorySeparatorChar, "Logs",
        serverName + "{Date}" + ".log");

      cfg.ReadFrom.Configuration(ctx.Configuration).MinimumLevel.Debug().MinimumLevel
        .Override("Microsoft", LogEventLevel.Information).WriteTo
        .RollingFile(path,
          outputTemplate:
          "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Application} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}"); //,fileSizeLimitBytes:500*1024*1024);
    }
  }
}