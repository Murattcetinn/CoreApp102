using CoreApp102.Api.DTOs;
using CoreApp102.Api.Filters;
using CoreApp102.Core.Repository;
using CoreApp102.Core.Services;
using CoreApp102.Core.UnitOfWork;
using CoreApp102.Data;
using CoreApp102.Data.Repository;
using CoreApp102.Data.UnitOfWork;
using CoreApp102.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp102Api
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
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ProductNotFoundFilter>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(CoreApp102.Service.Services.Service<>));
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration
                    ["ConnectionStrings:SqlConStr"].ToString(),o=> 
                    {
                        o.MigrationsAssembly("CoreApp102.Data");
                    });

            });

            services.AddControllers(o=> 
            {
                o.Filters.Add(new ValidationFilter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreApp102Api", Version = "v1" });
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreApp102Api v1"));
            }
            app.UseExceptionHandler(config=>
            {
                config.Run(async context=>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error!=null)
                    {
                        var ex = error.Error;
                        if (ex!=null)
                        {
                            ErrorDto errorDto = new ErrorDto();
                            errorDto.Status = 500;
                            errorDto.Errors.Add(ex.Message);
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));
                        }
                    }
                });
            });

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
