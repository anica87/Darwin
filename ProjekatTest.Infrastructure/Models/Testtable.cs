using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;
using ProjekatTest.Infrastructure.Interfaces;

namespace ProjekatTest.Infrastructure.Models {

    // IEntity dodat naknadno
    public class Testtable: IEntity {
        public virtual int Id { get; set; }
        public virtual Testtable2 Testtable2 { get; set; }
        public virtual string Name { get; set; }
    }
}
