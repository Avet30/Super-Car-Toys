using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository
{ 
    public class CarTypeRepository : Repository<CarType>, ICarTypeRepository //Je dois explicitement préciser d'implémenter la classe Repository<Category>
    {
        private AppDbContext _db;

        public CarTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CarType obj)
        {
            _db.CarTypes.Update(obj);
        }
    }
}
