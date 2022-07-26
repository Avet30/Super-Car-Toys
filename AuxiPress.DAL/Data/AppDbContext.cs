using AuxiPress.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuxiPress.DAL
{
    public class AppDbContext : IdentityDbContext //Je vais hériter de IdentityDbContext de EF, j'installe le Nuget Entity Framework Core + identity pour faire des user différents
    {
        //je crée un constructeur de cette classe pour établir ma connection
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Je configure mon AppDbContext et je passe ces paramètres à ma classe de base DbContext de Entity Framework
        {
        }
        public DbSet<Category> Categories { get; set; }//Je crée un DbSet dans mon AppDbContext pour pouvoir créer la table Category!

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet <Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }

    }
}

