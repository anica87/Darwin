using System.Collections.Generic;
using ProjekatTest.Infrastructure.Models;

namespace ProjekatTest.Infrastructure.Interfaces.Services
{
    public interface IPersonService
    {
        ICollection<Testtable> GetAllPersons();

        Testtable GetPersonById(int id);

        ICollection<Testtable2> GetAllPersonTypes();
    }
}
