using ProjekatTest.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NHibernate;
using ProjekatTest.DAL.UnitOfWork;
using NHibernate.Linq;

namespace ProjekatTest.DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {

        protected ISession Session { get { return NhUnitOfWork.Current.Session; } }

        public void Add(T item)
        {
            Session.Save(item);
        }

        public void Delete(T item)
        {
            Session.Delete(Session.Load<T>(item.Id));
        }

        public bool Exists(int id)
        {
            return Session.Query<T>().Count(x => x.Id == id) > 0;
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>() ;
        }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public IQueryable<T> Fetch(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> target;
            if (id < 0)
                target = Session.Query<T>();
            else
                target = Session.Query<T>().Where(x => x.Id == id);

            if (includes != null)
                foreach (Expression<Func<T, object>> includeProperty in includes)
                {
                        target = target.Fetch(includeProperty); // treba videti da li radi za array
                }

            return target;
        }

        public IQueryable<T> GetFor(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>().Where(predicate);
        }

        public void Update(T item)
        {
            Session.Update(item);
        }
    }
}
