﻿using System;
using System.IO;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyBooks.Api.Mapping;
using MyBooks.Api.Options;
using MyBooks.Api.Services;
using MyBooks.Dal.Context;
using MyBooks.Bll.Exceptions;
using MyBooks.Bll.Services;
using Newtonsoft.Json;

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
            // Swagger
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyBooks API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "toth.dani9204@gmail.com",
                        Name = "Daniel Toth"
                    }
                });
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MyBooks.Api.xml"));
            });

            // Problem details
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
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
            }); 

            // Health checks
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            // Mvc, only need controllers
            services.AddControllers(o => o.MaxModelValidationErrors = 50)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // API versioning
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            // Authentication
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            //    {
            //        options.Authority = "http://localhost:5001";
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = "myBooksApi";
            //        options.ApiSecret = "secret";
            //    });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "myBooksApi";
                });

            // Authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(
            //        "AdminOnly",
            //        policy => policy.RequireClaim("Level", "Admin"));
            //});

            // Hangfire
            //services.AddHangfire(config =>
            //{
            //    config
            //        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //        .UseSimpleAssemblyNameTypeSerializer()
            //        .UseRecommendedSerializerSettings()
            //        .UseSqlServerStorage(
            //            Configuration.GetConnectionString("HangfireConnection"),
            //            new SqlServerStorageOptions
            //            {
            //                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //                QueuePollInterval = TimeSpan.Zero,
            //                UseRecommendedIsolationLevel = true,
            //                UsePageLocksOnDequeue = true,
            //                DisableGlobalLocks = true
            //            });
            //});

            //services.AddHangfireServer();

            // Services
            services.Configure<GoodreadsOptions>(Configuration.GetSection("Goodreads"));
            services.AddHttpClient<IGoodreadsService, GoodreadsService>();

            services.AddSingleton(MapperConfig.Configure());
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                app.UseExceptionHandler("/error");
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseHealthChecks("/health");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBooks API v1"));

            //app.UseHangfireDashboard();
            //RecurringJob.AddOrUpdate(
            //    () => Console.WriteLine($"Recurring job executed at {DateTime.Now}"),
            //    Cron.Minutely);

            app.UseProblemDetails();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //context.Seed();
        }
    }
}
