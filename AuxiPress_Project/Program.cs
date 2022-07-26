
using AuxiPress.DAL;
using AuxiPress.DAL.Repository;
using AuxiPress.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using AuxiPress.BLL;

namespace AuxiPress_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /*On configure pour utiliser SQL server et ma connection string PUIS je rajoute les options et configure pour qu'il utilise UseSqlServer(Installer Entity.Framework.tools) PUIS je vais utiliser la m�thode existante GetConnectionString pour rajouter la connexion */
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //Je passe via UnitOfWork pour pouvoir facilement ajouter d'autres Repo dans le futur en utilisant cet injection de d�pendance, me permet d'�viter de refaire d'autres injections!
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddRazorPages();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });


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
            app.UseAuthentication();

            app.UseAuthorization();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}