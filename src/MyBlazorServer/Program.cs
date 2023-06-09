using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlazorServer.Shared;
using System.Diagnostics.Metrics;

namespace MyBlazorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var builder = WebApplication
                .CreateBuilder(args);

           builder.Host.UseLamar((_, registry) =>
           {
               registry.AddMvc();
               registry.AddLogging();
               registry.Scan(s =>
               {
                   s.TheCallingAssembly();
                   s.AssemblyContainingType<Program>();
                   s.WithDefaultConventions();
                   s.LookForRegistries();
               });
           });

           builder.Logging.ClearProviders();
           builder.Logging.AddConsole();

           builder.Services.AddRazorPages();
           builder.Services.AddServerSideBlazor(options =>
           {
               options.RootComponents.RegisterCustomElement<CustomTag>("custom-tag");
           });
            
           builder.Services.AddControllers();
           builder.Services.AddHttpContextAccessor();
          
            var app = builder.Build();

            var env = app.Environment;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ErrorMessage");
                app.UseHsts();
            }
            
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseWebSockets();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });


            app.Run();
        }

      
    }
}
