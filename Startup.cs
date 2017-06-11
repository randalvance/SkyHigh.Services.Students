using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SkyHigh.Services.Students.Data;
using SkyHigh.Services.Students.Options;
using SkyHigh.Services.Students.Repositories;

namespace SkyHigh.Services.Students
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<EndpointOptions>(Configuration.GetSection("Endpoint"));

            // Environment variable should be ConnectionStrings:DefaultConnection
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<StudentDbContext>(options => options.UseNpgsql(connectionString));

            services.AddSingleton<StudentRepository>();

            // Add framework services.
            services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // we need to wait for postgre to full start before calling this
            // using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            // {
            //     scope.ServiceProvider.GetService<StudentDbContext>().Database.Migrate();
            // }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); // TODO: Change this
            });
            app.UseMvc();
        }
    }
}
