using _01_Domain._01_Entities;
using _01_Domain._02_Contracts.AppServices;
using _01_Domain._02_Contracts.Repositories;
using _01_Domain._02_Contracts.Services;
using Domain.AppService;
using Domain.Service;
using EndPoint.MVC.Areas.Identity.Pages.Account;
using InfraStructure.Common;
using InfraStructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ToDoListDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ToDoListDbContextConnection' not found.");;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


////builder.Services.AddSignInManager<SignInManager<User>>();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<IMyTaskRepository, MyTaskRepository>();
builder.Services.AddScoped<IMyTaskService, MyTaskService>();
builder.Services.AddScoped<IMyTaskAppService, MyTaskAppService>();



var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection("SiteSetting").Get<SiteSetting>();
builder.Services.AddSingleton(siteSettings);








builder.Services.AddDbContext<ToDoListDbContext>(options =>
    options.UseSqlServer(siteSettings.ConnectionString)
);

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    
    options.SignIn.RequireConfirmedEmail = false;


})
    .AddEntityFrameworkStores<ToDoListDbContext>()
    .AddDefaultTokenProviders();



//builder.Services.AddIdentity<User, IdentityRole<int>>()
//    .AddRoles<IdentityRole<int>>()
//    .AddEntityFrameworkStores<ToDoListDbContext>();

//builder.Services.Configure<SiteSetting>(builder.Configuration.GetSection("SiteSetting"));
//builder.Services.AddDbContext<ToDoListDbContext>();

//builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//})
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();
