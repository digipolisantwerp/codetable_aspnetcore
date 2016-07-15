using Digipolis.Codetable;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebTest.DataAccess;

namespace WebTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure DataAccess service
            var connection = @"Server=localhost;Port=5432;Database=webtest;User Id=postgres;Password=P0stgr3s;";
            services.AddDbContext<MyDataContext>(options => options.UseNpgsql(connection));

            //Use the default CodetableDiscoveryOptions
            //services.AddCodetableDiscovery();

            //Use the custom CodetableDiscoveryOptions
            services.AddCodetableDiscovery(options =>
            {
                options.Route = "custom/codetables";
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseCodetableDiscovery();
        }
    }
}
