using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pharmacy.API.Infrastructure;
using Pharmacy.Service.MedicineServiceLayer;
using Pharmacy.Service.PrescriptionServiceLayer;
using Pharmacy.Service.UserServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.API
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
            //Mapper in konfigusrayonuna kendi mapperimin konfigurasyonunu veriyorum. Kendi mapperimi kullanacagimi bildiriyorum.
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(profile: new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper(); // mapper olusturuyorum
            services.AddSingleton(mapper); //projeme injekt ediyorum

            //Dependency Injection
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMedicineService, MedicineService>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pharmacy.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
