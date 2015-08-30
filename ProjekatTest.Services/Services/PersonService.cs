using ProjekatTest.Infrastructure.Attributes;
using ProjekatTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ProjekatTest.Infrastructure.Models;
using ProjekatTest.DAL.Database;
using ProjekatTest.DAL;
using ProjekatTest.DAL.UnitOfWork;

namespace ProjekatTest.Services.Services
{
    public class PersonService : IPersonService
    {
        public ICollection<Testtable> GetAllPersons()
        {      
            using (var ctx = new UnitOfWorkImpl())
            {
                return new TestTableRepository().Fetch(-1, x => x.Testtable2).ToList();
            } 
        }

        public Testtable GetPersonById(int id)
        {
            using (var ctx = new UnitOfWorkImpl())
            {
                return new TestTableRepository().Fetch(id, x => x.Testtable2).SingleOrDefault();
            }
        }

        public ICollection<Testtable2> GetAllPersonTypes()
        {
            using (var ctx = new UnitOfWorkImpl())
            {
                return new TestTableRepository2().GetAll().ToList();
            }

        }       
    }
}
