using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Learn1.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
//using Serilog.Events;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Learn1.Helpers;

namespace Learn1
{
    public class Startup
    {
        public Startup(IConfiguration configuration) //, ILogger<Startup> Logger)
        {
            Configuration = configuration;
        } 

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc(
				config => {
					config.Filters.Add(typeof(CustomGlobalExceptionHandler));
				});
            services.AddDbContext<IA_DB_1Context>(options => options.UseSqlServer(Configuration.GetConnectionString("IA_DB_1Database")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			//app.UseExceptionHandler(
			//	options => {
			//		options.Run(
			//		async context => {
			//			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			//			context.Response.ContentType = "text/html";
			//			var ex = context.Features.Get<IExceptionHandlerFeature>();
			//			if (ex != null) {
			//			 //var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
			//			 //await context.Response.WriteAsync(err).ConfigureAwait(false);
			//			 Log.Error(ex.Error.Message);
			//				if (env.IsDevelopment()) {
			//					app.UseDeveloperExceptionPage();
			//					app.UseBrowserLink();
			//				} else {
			//					app.UseExceptionHandler("/Home/Error");
			//				}
			//			}
			//			//var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
			//			//await context.Response.WriteAsync(err).ConfigureAwait(false);
			//		});
			//	}
			//);

			if (env.IsDevelopment()) {

				//Log.Error
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
