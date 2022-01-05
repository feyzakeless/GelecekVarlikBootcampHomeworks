using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pharmacy.API.Controllers;
using Pharmacy.API.Infrastructure;
using Pharmacy.Service.Job;
using Pharmacy.Service.MedicineServiceLayer;
using Pharmacy.Service.PrescriptionServiceLayer;
using Pharmacy.Service.UserServiceLayer;
using System;

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
            //Hangfire kuruldu
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();

            
            services.AddSingleton<IPrintJob, PrintJob>();

            //Mapper in konfigusrayonuna kendi mapperimin konfigurasyonunu veriyorum. Kendi mapperimi kullanacagimi bildiriyorum.
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(profile: new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper(); // mapper olusturuyorum
            services.AddSingleton(mapper); //projeme injekt ediyorum

            //Dependency Injection
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMedicineService, MedicineService>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<IEmailOperation, EmailOperation>();


            services.AddControllers();
            
            //Redis tanýmladýk
            services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");
            //services.AddMemoryCache();  //InMemoryCache i belirtiyoruz


            services.AddScoped<LoginFilter>();
            services.AddScoped<AuthorizationFilter>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pharmacy.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IBackgroundJobClient backgroundJobClient, 
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
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

            app.UseHangfireDashboard();
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire Job!"));
            recurringJobManager.AddOrUpdate(
                "Run every minute", 
                () => serviceProvider.GetService<IPrintJob>().Print(),
                "* * * * * "
                );
            recurringJobManager.AddOrUpdate("EmailOperation", 
                () => serviceProvider.GetService<IEmailOperation>().sendEmail(),
                Cron.Daily); //hergün sendEmail methodunu çalýþtýrýyoruz
        }
    }
}
