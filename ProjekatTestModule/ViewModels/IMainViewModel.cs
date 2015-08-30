using ProjekatTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTestModule.ViewModels
{
    public interface IMainViewModel
    {
        void AddUsers(ICollection<Testtable> list);
    }
}
