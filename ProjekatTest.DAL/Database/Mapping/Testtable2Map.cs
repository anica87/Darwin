using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using ProjekatTest.Infrastructure.Models; 

namespace ProjekatTest.DAL.Database.Mapping {
    
    
    public class Testtable2Map : ClassMap<Testtable2> {
        
        public Testtable2Map() {
			Table("TestTable2");
			LazyLoad();
			Id(x => x.Id).GeneratedBy.Identity().Column("Id");
			Map(x => x.Tipname).Column("TipName").Not.Nullable();
        }
    }
}
