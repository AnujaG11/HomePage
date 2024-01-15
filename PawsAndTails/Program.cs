using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsAndTails.Data;
using PawsAndTails.Models;
using HomePage.Data;
using HomePage.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace PawsAndTails
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<aspnetPawsAndTailsbf7e490958394e22832749b4b03c6426Context>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<cdacContext>(options =>
            //   options.UseMySql(builder.Configuration.GetConnectionString("MySqlConn")));
            {
                var connectionString = builder.Configuration.GetConnectionString("MySqlConn");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            /*
            var MySqlConnectionString = builder.Configuration.GetConnectionString("MySqlConnection");

            builder.Services.AddDbContext<AppDbContextMySQL>(options =>
            options.UseMySql(MySqlConnectionString, ServerVersion.AutoDetect(MySqlConnectionString)));
            */
            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MySqlConn");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            /*
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AppDbContextMySQL>();
            */
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "User", "Seller" };

                foreach(var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            
           using (var scope = app.Services.CreateScope())
            {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var user = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                };
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    await userManager.CreateAsync(user, "Admin@1234");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }



                app.Run();
        }
    }
}