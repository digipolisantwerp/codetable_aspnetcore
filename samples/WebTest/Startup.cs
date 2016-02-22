using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toolbox.Codetable;
using Toolbox.DataAccess;
using WebTest.DataAccess;
using Toolbox.DataAccess.Postgres;
using WebTest.Config;

namespace WebTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure the automapper
            AutoMapperConfiguration.Configure();

            //Configure DataAccess service
            var connString = new ConnectionString("localhost", 5432, "webtest", "postgres", "P0stgr3s");
            services.AddDataAccess<MyDataContext>(opt =>
            {
                opt.ConnectionString = connString;
                opt.DbConfiguration = new PostgresDbConfiguration();
                opt.DefaultSchema = "public";
                opt.PluralizeTableNames = false;
            });

            //Use the default CodetableDiscoveryOptions
            //services.AddCodetableDiscovery();

            //Use the custom CodetableDiscoveryOptions
            services.AddCodetableDiscovery(options =>
            {
                options.Route = "custom/codetables";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            app.UseIISPlatformHandler();

            app.UseMvc();

            app.UseCodetableDiscovery();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
