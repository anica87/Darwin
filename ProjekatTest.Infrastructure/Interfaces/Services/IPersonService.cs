using ProjekatTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTest.Infrastructure.Interfaces
{
    public interface IPersonService
    {
        ICollection<Testtable> GetAllPersons();

        Testtable GetPersonById(int id);

        ICollection<Testtable2> GetAllPersonTypes();
    }
}
