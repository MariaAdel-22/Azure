using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCocheCosmosDb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCocheCosmosDb
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
            string cadenaConexion = this.Configuration.GetValue<string>("CosmosDb:connectionString");

            string databaseName = this.Configuration.GetValue<string>("CosmosDb:databaseName");

            string containerName= this.Configuration.GetValue<string>("CosmosDb:containerName");

            //CREAMOS EL CLIENTE DE COSMOS CON LA CADENA DE CONEXION
            CosmosClient cosmosClient = new CosmosClient(cadenaConexion);

            //INSTANCIAMOS UN CONTENEDOR RECUPERADOR A PARTIR DE NOMBRE DE BBDD Y NOMBRE DE CONTENEDOR
            Container containerCosmos = cosmosClient.GetContainer(databaseName, containerName);

            services.AddSingleton<CosmosClient>(x => cosmosClient);
            services.AddTransient<Container>(XmlConfigurationExtensions => containerCosmos);
            services.AddTransient<ServiceCocheCosmos>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
