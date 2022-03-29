using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAlumnoPractica.Data;
using WebApiAlumnoPractica.Helpers;
using WebApiAlumnoPractica.Repositories;

namespace WebApiAlumnoPractica
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
            string azureSql =this.Configuration.GetConnectionString("cadenaazure");
            services.AddTransient<RepositoryAlumno>();
            services.AddDbContext<AlumnoContext>(options => options.UseSqlServer(azureSql));

            HelperAlumnoToken helper = new HelperAlumnoToken(this.Configuration);
            services.AddAuthentication(helper.GetAuthenticationOptions())
                .AddJwtBearer(helper.GetJwtOptions());

            services.AddTransient<HelperAlumnoToken>
                (x => helper);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Alumno",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiAlumnoPractica v1");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
