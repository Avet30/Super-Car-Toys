using AuxiPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category> //Je le fais hériter des méthodes dans mon IRepository Generic
    {
        //UPDATE
        void Update(Category obj);
    }
}
