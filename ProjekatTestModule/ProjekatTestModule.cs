using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ProjekatTestModule.ViewModels;
using Microsoft.Practices.ServiceLocation;
using ProjekatTest.Infrastructure.Interfaces;
using ProjekatTest.Services.Services;

namespace ProjekatTestModule
{
    public class ProjekatTestModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private readonly IEventAggregator evt;


        public ProjekatTestModule(IRegionManager regionManager, IUnityContainer container, IEventAggregator evt)
        {
            this.regionManager = regionManager;
            this.container = container;
            this.evt = evt;
        }

        public void Initialize()
        {
            container.RegisterType<IPersonService, PersonService>();

            container.RegisterType<IMainViewModel, MainViewModel>();
            container.RegisterType<IDetailViewModel, DetailViewModel>();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.ModulAMainView));
            regionManager.RegisterViewWithRegion("DetailRegion", typeof (Views.ModulAViewDetail));
        }
    }
}
