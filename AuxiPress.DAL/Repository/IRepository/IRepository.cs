using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null); 

        void Add(T entity); //Rajouter une entitée

        void Remove(T entity); //Retirer une entitée

        void RemoveRange(IEnumerable<T> entities); //Retirer plusieurs entitées (j'utilise IEnumerable car j'aurais une collection d'entitée!)

    }
}
