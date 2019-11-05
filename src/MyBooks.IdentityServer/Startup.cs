using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBooks.Dal.Context;

namespace MyBooks.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers and views
            services.AddControllersWithViews();

            // DBContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"));
            });

            // Identity
            //services.AddIdentityCore<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            // IdentityServer4
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                // This adds the config data from DB (clients, resources)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.Clients);
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = b =>
                //        b.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = b =>
                //        b.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
                //})
                //.AddAspNetIdentity<ApplicationUser>();

            if (_environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            // Authentication
            //services.Configure<GoogleOptions>(Configuration.GetSection("Authentication:Google"));
            //services.AddAuthentication()
            //    .AddGoogle();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseRouting();

            app.UseEndpoints(builder => { builder.MapDefaultControllerRoute(); });
        }
    }
}
