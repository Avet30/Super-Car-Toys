using AuxiPress.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db); //A chaque création de CategoryRepo j'utilise ma DB via Entity
            CarType = new CarTypeRepository(_db);
            Product = new ProductRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public ICarTypeRepository CarType { get; private set; }
        public IProductRepository Product { get; private set; }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

