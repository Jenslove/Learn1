﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Learn1
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var host = BuildWebHost(args);

			//this is only for use by the logger creation to reach the appsetting.json
			var configuration = new ConfigurationBuilder()
				 .SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("appsettings.json")
							.Build();

			Log.Logger = new LoggerConfiguration()
				 .MinimumLevel.Debug()
				 .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
				 .Enrich.FromLogContext()
				 .WriteTo.Console()
				 .WriteTo.MSSqlServer(connectionString: configuration.GetConnectionString("IA_DB_1Database"),
												tableName: "Log",
												autoCreateSqlTable: true,
												period: new TimeSpan(0, 0, 5)
											)
				 .WriteTo.RollingFile("./Logs/log-{Date}.txt", shared: true)
				 .CreateLogger();

			//Log.Debug("ConStr{a}", configuration.GetConnectionString("IA_DB_1Database"));
			Log.Debug("About to start Website.");

			try {
				host.Run();
			} catch (Exception ex) {
				Log.Error(ex, "Exception");
			}
		}

		public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
					 .UseKestrel()
					 .UseIISIntegration()
					 .UseContentRoot(Directory.GetCurrentDirectory())
					 .UseStartup<Startup>()
					 .UseSerilog()
                .Build();
	}
}
