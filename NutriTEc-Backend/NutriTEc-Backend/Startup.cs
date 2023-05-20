﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NutriTEc_Backend.Repository;
using Microsoft.EntityFrameworkCore;
using NutriTEc_Backend.Repository.Interface;
using NutriTEc_Backend.Repository.DataModel;

namespace NutriTEc_Backend
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddRazorPages();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<INutriTEcRepository, NutriTEcRepository>(); 
            services.AddDbContext<NutriTecContext>(o => o.UseNpgsql(configRoot.GetConnectionString("NutriTEc")));

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
