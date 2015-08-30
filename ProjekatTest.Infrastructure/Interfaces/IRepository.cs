using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTest.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {

        // CRUD
        T GetById(int id);
        IQueryable<T> GetAll();
        void Add(T item);
        void Delete(T item);
        void Update(T item);


        IQueryable<T> Fetch(int id, params Expression<Func<T, object>>[] includes);
            
        IQueryable<T> GetFor(Expression<Func<T, bool>> predicate);

        bool Exists(int id);
    }
}
