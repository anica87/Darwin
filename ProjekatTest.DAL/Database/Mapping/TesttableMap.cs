using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using ProjekatTest.Infrastructure.Models; 

namespace ProjekatTest.DAL.Database.Mapping {
    
    
    public class TesttableMap : ClassMap<Testtable> {
        
        public TesttableMap() {
			Table("TestTable");
			LazyLoad();
			Id(x => x.Id).GeneratedBy.Identity().Column("Id");
			References(x => x.Testtable2).Column("TipId");
			Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
