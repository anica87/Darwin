using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ProjekatTest.Infrastructure.Interfaces.Services;
using ProjekatTest.ModuleShared.ViewModels;
using ProjekatTest.ModuleShared.Views;
using ProjekatTest.Services.Services;

namespace ProjekatTest.ModuleShared
{
    /*
        TBD TBD!
    */


    public class ModuleShared: IModule
    {

        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private readonly IEventAggregator evt;

        public ModuleShared(IRegionManager regionManager, IUnityContainer container, IEventAggregator evt)
        {
            this.regionManager = regionManager;
            this.container = container;
            this.evt = evt;
        }

        public void Initialize()
        {
            container.RegisterType<IPersonService, PersonService>();

            container.RegisterType<IMenuModuleShared, MenuModuleShared>();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(ModuleSharedMenu));

            container.RegisterType<IStatusBarModuleShared, StatusBarModuleShared>();
            regionManager.RegisterViewWithRegion("StatusRegion", typeof(ModuleSharedStatusBar));
        }





    }
}
