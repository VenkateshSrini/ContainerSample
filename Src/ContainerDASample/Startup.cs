﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ContainerDASample.Database;
using ContainerDASample.Database.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

namespace ContainerDASample
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
            //var redis=  ConnectionMultiplexer.Connect($"{Configuration["RedisOptions:host"]}:{Configuration["RedisOptions:port"]}");
            //services.AddDataProtection()
            //    .PersistKeysToRedis(redis, "DataProtection-Keys");
            services.AddOptions();
            services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.AddMvc();
            DIRegistration.RegisterRepository(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            //app.UseMvc(routes=> 
            //routes.MapRoute("default", "{controller}/{action}/{id?}")

            //);
        }
    }
}
