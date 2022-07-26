using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository
{ 
    public class ProductRepository : Repository<Product>, IProductRepository //Je dois explicitement préciser d'implémenter la classe Repository<Category>
    {
        private AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            //_db.Products.Update(obj); Fonction de base via Entity (Pas intéressant via Entity car il va faire un update sur toutes les propriétés même si on ne que changer 1 seule propriété!

            //On va utiliser directement notre Base de donnée (Ok car on est dans notre Repository et notre DAL)

            var ObjFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(ObjFromDb != null)
            {
                ObjFromDb.Name = obj.Name;
                ObjFromDb.Description = obj.Description;
                ObjFromDb.Maker = obj.Maker;
                ObjFromDb.EAN = obj.EAN;          
                ObjFromDb.Price = obj.Price;
                ObjFromDb.Price50 = obj.Price50;
                ObjFromDb.Price100 = obj.Price100;
                ObjFromDb.ListPrice = obj.ListPrice;
                ObjFromDb.CarTypeId = obj.CarTypeId;
                ObjFromDb.CategoryId = obj.CategoryId;
                if(obj.ImageURL != null)
                {
                    ObjFromDb.ImageURL = obj.ImageURL;
                }

            }
        }
    }
}
