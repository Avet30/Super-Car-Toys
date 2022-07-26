using AuxiPress.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuxiPress.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet; //

        public Repository(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        //ADD
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //READ //includeProp - "Category,Cartype" (la regle de ma repo)
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(includeProperties != null)
            {
                foreach(var inclueProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclueProp);
                }
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);  //Je filtre mon data avec le Where (cela me permettra de rajouter d'autres paramètres plus tard)
            if (includeProperties != null)
            {
                foreach (var inclueProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclueProp);
                }
            }

            return query.FirstOrDefault(); //On retournera le FirstOrDefault
        }
        //DELETE
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
