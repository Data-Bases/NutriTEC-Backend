using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NutriTEc_Backend.Repository;
using Microsoft.EntityFrameworkCore;
using NutriTEc_Backend.Repository.Interface;
using NutriTEc_Backend.DataModel;
using AutoMapper;
using Microsoft.Extensions.Options;
using NutriTEc_Backend.Entities;

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
            services.Configure<ForumDatabaseSettings>(
                configRoot.GetSection(nameof(ForumDatabaseSettings)));
            services.AddSingleton<ICommentsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ForumDatabaseSettings>>().Value);
            services.AddScoped<INutriTEcRepository, NutriTEcRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddDbContext<NutriTecContext>(o => o.UseNpgsql(configRoot.GetConnectionString("NutriTEc")));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();



        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("https://mango-sky-02f714210.3.azurestaticapps.net");
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
