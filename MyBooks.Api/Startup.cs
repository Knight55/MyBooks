﻿using System;
using System.IO;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBooks.Api.Mapping;
using MyBooks.Bll.Context;
using MyBooks.Bll.Entities;
using MyBooks.Bll.Exceptions;
using MyBooks.Bll.Services;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace MyBooks.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
            });

            // Swagger
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info
                {
                    Title = "MyBooks API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Email = "toth.dani9204@gmail.com",
                        Name = "Daniel Toth"
                    }
                });
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MyBooks.Api.xml"));
                o.DescribeAllEnumsAsStrings();
            });

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = ctx => false;
                options.Map<EntityNotFoundException>(ex => new ProblemDetails
                {
                    Title = "Invalid ID",
                    Status = StatusCodes.Status404NotFound
                });
            });

            // DBContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
                options.UseNpgsql(Configuration.GetConnectionString("NpgSqlServerConnection"));
            }); 

            // Health checks
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            // Mvc
            services.AddMvc(o => o.MaxModelValidationErrors = 50)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(json => json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // Authentication
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "myBooksApi";
                    options.ApiSecret = "secret";
                });

            // Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "AdminOnly",
                    policy => policy.RequireClaim("Level", "Admin"));
            });

            // Services
            services.AddSingleton(MapperConfig.Configure());
            services.AddTransient<IBookService, BookService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    //app.UseHsts();
            //    app.UseExceptionHandler("/error");
            //}

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseHealthChecks("/health");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBooks API v1"));

            app.UseAuthentication();

            app.UseProblemDetails();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //context.Seed();
        }
    }
}
