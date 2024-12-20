using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestoranBoshqaruvi.context;
using System;

namespace RestoranBoshqaruvi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddControllersWithViews();

            // Cookie Authentication Setup
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";  // Tizimga kirish uchun sahifa
                    options.LogoutPath = "/Account/Logout";  // Tizimdan chiqish
                    options.AccessDeniedPath = "/Account/AccessDenied";  // Ruxsat berilmagan sahifa
                });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));
            // Add session configuration
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session vaqtincha saqlash
                options.Cookie.IsEssential = true; // Cookie zarur
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Middleware to handle requests
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Enable session middleware to manage session state
            app.UseSession();

            // Enable Authentication and Authorization
            app.UseAuthentication(); // Enable Authentication
            app.UseAuthorization(); // Enable Authorization

            // Configure routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
