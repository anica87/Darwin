using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTest.Infrastructure.Attributes
{
    // Koristi se da se od nekog metoda napravi atomska transakcija

    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute: Attribute
    {

    }
}
