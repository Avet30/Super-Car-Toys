using AuxiPress.DAL.Repository.IRepository;
using AuxiPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private AppDbContext _db;

        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
