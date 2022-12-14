using Business.Abstract;
using Business.Concrete;
using Business.Configuration.Mapper;
using DataAccess.Absract;
using DataAccess.Concrete.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TODEBFinalProjectCrediCardAPI
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
            services.AddSingleton<MongoClient>(x => new MongoClient("mongodb://localhost:27017"));
            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddScoped<ICreditCardDal, CreditCardDal>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentDal, PaymentDal>();

            services.AddScoped<IPaidTypeService, PaidTypeService>();
            services.AddScoped<IPaidTypeDal, PaidTypeDal>();

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new MapperProfile());
            }, Assembly.GetExecutingAssembly());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TODEBFinalProjectCrediCardAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODEBFinalProjectCrediCardAPI v1"));
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
