using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ProjekatTest.Infrastructure.Interfaces.Services;
using ProjekatTest.ModuleConfigurations.ViewModels;
using ProjekatTest.ModuleConfigurations.Views;
using ProjekatTest.Services.Services;

namespace ProjekatTest.ModuleConfigurations
{
    public class ModuleConfigurations : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private readonly IEventAggregator evt;


        public ModuleConfigurations(IRegionManager regionManager, IUnityContainer container, IEventAggregator evt)
        {
            this.regionManager = regionManager;
            this.container = container;
            this.evt = evt;
        }

        public void Initialize()
        {
            container.RegisterType<IPersonService, PersonService>();

            container.RegisterType<IDetailViewModelModuleConfigurations, DetailViewModelModuleConfigurations>();
            regionManager.RegisterViewWithRegion("DetailRegion", typeof(ModuleConfigurationsDetail));
        }
    }
}
