using Application.Filters;
using Application.Interfaces.Services;
using Application.Services.v1;
using Application.Validators;
using Domain.DTO;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackEnd.Cobranca
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

            services.AddScoped<ICobrancaAppService, CobrancaAppService>();
            services.AddScoped<IClienteAppService, ClienteAppService>();
             
            services.AddControllers()
            .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining(typeof(CalcularValorTotalValidator));
            }).AddNewtonsoftJson();

        services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Serviços API",
                    Version = "v1",
                    Description = "Serviço de cobranças",
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
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviços de cobrança"));

        }
    }
}
