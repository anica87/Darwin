using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;

namespace ProjekatTest.ModuleShared
{
    /*
        TBD TBD!
    */


    public class ModuleShared: IModule
    {
        private readonly IEventAggregator evt;

        public ModuleShared(IEventAggregator  evt)
        {
            this.evt = evt;
        }

        public void Initialize()
        {
            
        }
    }
}
