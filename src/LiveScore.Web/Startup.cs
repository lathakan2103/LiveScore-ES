﻿using LiveScore.Application.Services.Match;
using LiveScore.Infrastructure;
using LiveScore.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiveScore.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddTransient<IMatchControllerService, MatchControllerService>();
			services.AddTransient<IEventRepository, EventRepository>();
			services.AddDbContext<WaterpoloContext>(options => options.UseSqlServer(Configuration.GetConnectionString("naa4e_Waterpolo")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Match}/{action=Index}/{id?}");
			});
			BusConfig.Initialize();
			//RavenDbConfig.Initialize();
		}
	}
}
