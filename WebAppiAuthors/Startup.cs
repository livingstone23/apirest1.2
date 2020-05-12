using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
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
using Newtonsoft.Json;
using WebAppiAuthors.Contexts;
using WebAppiAuthors.Entities;
using WebAppiAuthors.Helpers;
using WebAppiAuthors.Models;

namespace WebAppiAuthors
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
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Author, AuthorDTO>();
                configuration.CreateMap<AuthorCreationDTO, Author>();
                configuration.CreateMap<Author, AuthorDTO>();

            },typeof(Startup));

            services.AddResponseCaching();
            //services.AddAuthentication();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(config => { config.Conventions.Add(new ApiExplorerGroupPerVersionConvention()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                //.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)

            services.AddControllers();
            services.AddSwaggerGen(config =>
            {


                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Mi Web API",
                    Description = "Esta es una descripción del Web API",
                    TermsOfService = new Uri("https://www.udemy.com/user/felipegaviln/"),
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("http://bfy.tw/4nqh")
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Felipe Gavilán",
                        Email = "felipe_gavilan887@hotmail.com",
                        Url = new Uri("https://gavilan.blog/")
                    }
                });

                config.SwaggerDoc("v2", new OpenApiInfo { Title = "Mi Web API", Version = "v2" });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //config.IncludeXmlComments(xmlPath);


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
                config.SwaggerEndpoint("/swagger/v2/swagger.json", "Mi API V2");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCaching();
            //app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
