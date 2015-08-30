using ProjekatTest.Infrastructure.Interfaces;
using ProjekatTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ProjekatTest.DAL.Repository;

namespace ProjekatTest.DAL.Database
{
    public class TestTableRepository : RepositoryBase<Testtable>, ITesttableRepository
    {
           
    }
}
