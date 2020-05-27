using AutoMapper;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Data.IRepositories;
using Hahn.ApplicatonProcess.May2020.Data.Repository;
using Hahn.ApplicatonProcess.May2020.Domain.IService;
using Hahn.ApplicatonProcess.May2020.Domain.Service;
using Hahn.ApplicatonProcess.May2020.Domain.Utility;
using Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hahn.ApplicatonProcess.May2020.Web
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
            services.AddControllers();

            services.AddDbContext<HahnDataContext>(options => options.UseInMemoryDatabase(databaseName: Configuration["DatabaseName"]));

            services.AddAutoMapper(typeof(Startup));

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            #region Repositories

            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();

            #endregion Repositories

            #region Service

            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<ICountryService, CountryService>();

            #endregion Service

            services.Configure<AppConfigurationData>(Configuration.GetSection("Settings"));

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errors = c.ModelState.Where(x => x.Value.Errors.Count > 0).Select(p => new { field = p.Key, error = p.Value.Errors.Select(t => t.ErrorMessage) });

                    return new BadRequestObjectResult(errors.ToList());
                };
            })
             .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicantRequestValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hahn Application Processing API",
                    Description = "Application Api Developed by Ogunseye Timilehin Mayowa ",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ogunseye Timilehin Mayowa",
                        Email = string.Empty,
                        Url = new Uri("https://github/teecode"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://github/teecode"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn API doc.");
                        });

            app.UseSerilogRequestLogging();


            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              );



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}