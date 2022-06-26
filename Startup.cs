using BusBoi_Backend.Data.Options;
using BusBoiBackend.GraphQL;
using BusBoiBackend.OneBusAway;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend
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
            // Add One Bus Away Service and options
            services.AddHttpClient<OneBusAwayService>();
            //services.Configure<OneBusAwayServiceOptions>(Configuration.GetSection(OneBusAwayServiceOptions.Section));

            // Add One Bus Away Handler
            services.AddTransient<OneBusAwayHandler>();

            // Add Database
            services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("DatabaseConnection")));

            // Add Automapper
            services.AddAutoMapper(typeof(Startup));

            // Add CORS
            services.AddCors(options =>
            {
                var frontendUrl = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(frontendUrl).AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Add GraphQL
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();

            // Add Authorization
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OneBusAwayHandler obaHandler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //obaHandler.SeedAllRoutes();

            //app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
