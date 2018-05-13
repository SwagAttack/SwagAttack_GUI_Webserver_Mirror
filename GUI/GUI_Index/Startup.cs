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
using GUI_Index.Session;
using Microsoft.AspNetCore.Http;

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

            services.AddTransient<IClientWrapper, Client>();

            var sp = services.BuildServiceProvider();
            services.AddTransient<IHttpRequestFactory, HttpRequestFactory>(f =>
                new HttpRequestFactory(sp.GetService<IClientWrapper>(),
                    "https://swagattackapi.azurewebsites.net/"));

            services.AddTransient<IUserProxy, UserProxy>();
            services.AddTransient<ILobbyProxy, LobbyProxy>();

            //for sessions injects to constructor
            services.AddScoped<IUserSession, UserSession>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //

            //services.AddSingleton<ISwagCommunication>(s => SwagCommunication.GetInstance("https://swagattkapi.azurewebsites.net/"));
            //SwagCommunication client = new SwagCommunication("https://swagattkapi.azurewebsites.net/");

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();  //enables sessions for the asp.net 
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

            app.UseSession();
            app.UseStaticFiles();

            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            app.UseSignalR(routes =>
            {
                routes.MapHub<LobbyHub>("/Hubs/LobbyHub"); //route to path of hub
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
