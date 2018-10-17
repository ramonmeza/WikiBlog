using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WikiBlog.Data;

namespace WikiBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC pattern
            services.AddMvc();

            // Connect to SQL database
            // Move this to a config file at some point
            var connection = @"Server=localhost\SQLEXPRESS;Database=WikiBlog;Trusted_Connection=True;";
            services.AddDbContext<PostsContext>(options => options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            // Setup MVC defaults
            app.UseMvc();
        }
    }
}
