using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Learn1
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var i = 1;
			var host = BuildWebHost(args); //.Run();

			Log.Logger = new LoggerConfiguration()
				 .MinimumLevel.Debug()
				 .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
				 .Enrich.FromLogContext()
				 .WriteTo.Console()
				 .WriteTo.RollingFile("./Logs/log-{Date}.txt", shared: true)
				 //.WriteTo.MSSqlServer(configuration.GetConnectionString("IA_DB_1Database"), "Log")
				 .CreateLogger();

			Log.Information("About to start Website.");

			host.Run();

			//var configuration = new ConfigurationBuilder()
			//	 .SetBasePath(Directory.GetCurrentDirectory())
			//            .AddJsonFile("appsettings.json")
			//            .Build();

			//      Log.Logger = new LoggerConfiguration()
			//          .MinimumLevel.Debug()
			//          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
			//          .Enrich.FromLogContext()
			//          .WriteTo.Console()
			//.WriteTo.RollingFile("log-{Date}.txt", shared: true)
			////.WriteTo.MSSqlServer(configuration.GetConnectionString("IA_DB_1Database"), "Log")
			//          .CreateLogger();

			//      try
			//      {
			//          Log.Information("Starting web host");

			//          var host = new WebHostBuilder()
			//              .UseKestrel()
			//              .UseIISIntegration()
			//              //.UseContentRoot(Directory.GetCurrentDirectory())
			//              .UseStartup<Startup>()
			//              .UseSerilog()  // <- The magic
			//              .Build();


			//          host.Run();

			//          //return 0;
			//      }
			//      catch (Exception ex)
			//      {
			//          Log.Fatal(ex, "Host terminated unexpectedly");
			//          //return 1;
			//      }
			//      finally
			//      {
			//          Log.CloseAndFlush();
			//      }
		}

		public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
