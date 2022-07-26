using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository
{ 
    public class CategoryRepository : Repository<Category>, ICategoryRepository //Je dois explicitement préciser d'implémenter la classe Repository<Category>
    {
        private AppDbContext _db;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
