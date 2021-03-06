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
using MMPSystemManager.DBContext;
using Microsoft.EntityFrameworkCore;
using MMPSystemManager.Module;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MMPSystemManager
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
                    
            services.AddDbContext<MMPContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
                       
            //Add session
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseMvc();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();
        }
    }
}
