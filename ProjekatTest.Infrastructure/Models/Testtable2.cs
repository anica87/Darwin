using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;
using ProjekatTest.Infrastructure.Interfaces;

namespace ProjekatTest.Infrastructure.Models {

    // IEntity dodat naknadno
    public class Testtable2: IEntity {
        public Testtable2() { }
        public virtual int Id { get; set; }
        public virtual string Tipname { get; set; }
    }
}
