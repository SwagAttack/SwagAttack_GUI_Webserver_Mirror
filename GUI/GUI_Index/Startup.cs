using GUI_Index;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GUICommLayer;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using GUI_Index.Hubs;

namespace GUI_Index
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
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            
            services.AddSingleton(s => Client.GetInstance ());
            services.AddTransient<IUserProxy, UserProxy>(s =>
            {
                return new UserProxy(Client.GetClientInstance());
            });
            //services.AddSingleton<ISwagCommunication>(s => SwagCommunication.GetInstance("https://swagattkapi.azurewebsites.net/"));
            //SwagCommunication client = new SwagCommunication("https://swagattkapi.azurewebsites.net/");

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            app.UseSignalR(routes =>
            {
                routes.MapHub<LobbyHub>("/LobbyHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=LogInd}/{id?}");
            });

        }
    }
}
