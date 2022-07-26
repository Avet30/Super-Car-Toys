
using AuxiPress.DAL;
using AuxiPress.DAL.Repository;
using AuxiPress.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AuxiPress_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /*On configure pour utiliser SQL server et ma connection string PUIS je rajoute les options et configure pour qu'il utilise UseSqlServer(Installer Entity.Framework.tools) PUIS je vais utiliser la méthode existante GetConnectionString pour rajouter la connexion */
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //Je passe via UnitOfWork pour pouvoir facilement ajouter d'autres Repo dans le futur en utilisant cet injection de dépendance, me permet d'éviter de refaire d'autres injections!


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
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}