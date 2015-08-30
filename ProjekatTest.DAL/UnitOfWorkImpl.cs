using NHibernate.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.SqlCommand;
using NHibernate.Type;
using System.Collections;
using NHibernate;
using ProjekatTest.DAL.UnitOfWork;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace ProjekatTest.DAL
{
    public class UnitOfWorkImpl: IDisposable
    {
        private bool disposed = false;

        private readonly ISessionFactory _sessionFactory;

        public UnitOfWorkImpl()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("connStringDatabase")))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(ProjekatTest.DAL.Database.Mapping.TesttableMap))))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(ProjekatTest.DAL.Database.Mapping.Testtable2Map))))
                .BuildSessionFactory();

            if (NhUnitOfWork.Current ==null)
            {
                NhUnitOfWork.Current = new NhUnitOfWork(_sessionFactory);
                NhUnitOfWork.Current.BeginTransaction();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                        try
                        {
                            NhUnitOfWork.Current.Commit();
                        }
                        catch
                        {
                            try
                            {
                                NhUnitOfWork.Current.Rollback();
                            }
                            catch
                            {

                            }

                            throw;
                        }
                        finally
                        {
                            NhUnitOfWork.Current = null;
                        }
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
