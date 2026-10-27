using DataLayer.Irepo;
using DataLayer.Models;
using DataLayer.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDb>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<Iuser, UserRepo>();
builder.Services.AddScoped<Ischool,SchoolRepo>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60); //this is time if we stay inactive session will dissapear
    options.Cookie.HttpOnly = true; // this means the cookie is only accessible via Http req no JS
    options.Cookie.IsEssential = true; //Cookie is essential for serivices to work
});

builder.Services.AddAuthentication(  //This  configure the authentication
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";  // this means if auth fails go here
    });



var app = builder.Build();


//using basically enables or add middleware to the pipeline
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication(); //I want to authenticate users using Cookies 
app.UseAuthorization(); // Authen before Autho cuz in AUthen the cokkie is checked decrypted and claims are retrived used for autho

app.MapStaticAssets(); //This means i can use static things present in wwwroot

//app.MapControllers(); //this is used for attribute routing 

//This is the default route that executes when the application starts
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}")
    .WithStaticAssets();


app.Run();
