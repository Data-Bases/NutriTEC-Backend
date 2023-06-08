using Microsoft.AspNetCore.Hosting;
using NutriTEc_Backend;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method
var app = builder.Build();
startup.Configure(app, builder.Environment); // calling Configure method