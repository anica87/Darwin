using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ProjekatTest.Infrastructure;
using ProjekatTest.Infrastructure.Interfaces.Services;
using ProjekatTest.ModuleConfigurations.ViewModels;
using ProjekatTest.ModuleConfigurations.Views;
using ProjekatTest.Services.Services;
using System.Windows;
using System.Windows.Controls;

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
            container.RegisterType<IMainMeniViewModel, MainMeniViewModel>();
            container.RegisterType<IScriptsViewModel, ScriptsViewModel>();
           
            

            Button btnConfiguration = new Button();
            btnConfiguration.Content = "Configuration";
            btnConfiguration.Height = 20;
            btnConfiguration.Width = 80;
            btnConfiguration.Margin = new Thickness(10, 49, 0, 0);
            btnConfiguration.HorizontalAlignment = HorizontalAlignment.Left;
            btnConfiguration.VerticalAlignment = VerticalAlignment.Top;

            Button btnScripts = new Button();
            btnScripts.Content = "Scripts";
            btnScripts.Width = 80;
            btnScripts.Height = 20;
            btnScripts.Margin = new Thickness(162, 49, 0, 0);
            btnScripts.HorizontalAlignment = HorizontalAlignment.Left;
            btnScripts.VerticalAlignment = VerticalAlignment.Top;
            var view = ServiceLocator.Current.GetInstance<Views.ModuleConfigurationsDetail>();
            regionManager.AddToRegion(RegionNames.DetailRegion, view);
            btnScripts.Click += (o, i) =>
            {

                ScriptsView scriptsView = new ScriptsView();
               
                var region = this.regionManager.Regions[RegionNames.DetailRegion];
                region.Add(scriptsView);

               
                if (region != null && view != null)
                {
                     region.Remove(view);
                     region.Activate(scriptsView);
                    
                }
            };
            Grid mainGrid = new Grid();
            mainGrid.Height = 100;
            mainGrid.Width = 300;
            mainGrid.VerticalAlignment = VerticalAlignment.Top;
            mainGrid.HorizontalAlignment = HorizontalAlignment.Left;
            mainGrid.Children.Add(btnConfiguration);
            mainGrid.Children.Add(btnScripts);

            regionManager.AddToRegion(RegionNames.MainRegion, mainGrid);

        }
    }
}
