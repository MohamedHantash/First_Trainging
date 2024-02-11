using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using train1.Models;
using train1.Repository;

namespace train1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add services to DB
            builder.Services.AddDbContext<ApplicationContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("MVC")));

            // Add services to userManager ,signinManager
           builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
           {
               options.Password.RequiredLength = 8;
               
           }).AddEntityFrameworkStores<ApplicationContext>();

           builder.Services.AddScoped<IUserRepository, UserRepository>();
           builder.Services.AddScoped<IAccountRepository, AccountRepository>();
           builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
           builder.Services.AddScoped<IProductRepository, ProductRepository>(); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=Account}/{action=Login}");
            app.Run();
        }
    }
}