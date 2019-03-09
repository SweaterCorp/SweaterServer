using System;
using System.IO;
using CommonLibraries.CommandLine;
using CommonLibraries.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace SweaterMain
{
  public class Program
  {
    public static CommandLinePattern CommandLine { get; private set; }
    public static IConfiguration Configuration { get; private set; }
    public static string Scheme { get; } = "http";
    public static string IpAddress { get; } = "localhost";
    public static string Port { get; private set; }
    public static string Url => Scheme + "://" + IpAddress + ":" + Port;

    public static void Main(string[] args)
    {
      CommandLine = Command.WithName("host").HasOption("-port", "-p").HasOption("-swagger", "-swg").HasOption("-currentdirectory", "-curdir");
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
      var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory);

      builder.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
      builder.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "..", "routingsettings.json"));
      builder.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "..", "hiddensettings.json"));

      Configuration = builder.Build();
      Port = Configuration["WebHost:Port"];

      

      if (CommandLine.TryParse(args, out var command) && command is HostCommand hostCommand &&
          !hostCommand.Port.IsNullOrEmpty()) Port = hostCommand.Port;

      return WebHost
        .CreateDefaultBuilder(args)
        .UseConfiguration(Configuration)
        .UseUrls(Url)
        .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Trace))
        .UseStartup<Startup>()
        .UseSerilog(ConfigureLogger)
        .UseStartup<Startup>()
        .Build();
    }

    private static void ConfigureLogger(WebHostBuilderContext ctx, LoggerConfiguration cfg)
    {
      var serverName = Configuration.GetValue<string>("ServerName");
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