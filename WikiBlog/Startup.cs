using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WikiBlog.Data;
using WikiBlog.Hubs;

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
            services.AddDbContext<PostsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PostsContext")));

            // Add SignalR
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            // Setup SignalR
            app.UseSignalR(routes => {
                routes.MapHub<PostHub>("/postHub");
            });

            // Setup MVC defaults
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Blog}/{action=Index}/{id?}"
                );
            });
        }
    }
}
