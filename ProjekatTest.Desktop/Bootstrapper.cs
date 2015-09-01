using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using ProjekatTest.Infrastructure.Interfaces;
using System.Windows;

namespace ProjekatTest.Desktop
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        // Ubacivanje modula //
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(ModuleConfigurations.ModuleConfigurations));
           // moduleCatalog.AddModule(typeof(ProjekatTestModule.ProjekatTestModule));
            moduleCatalog.AddModule(typeof (ProjekatTest.ModuleShared.ModuleShared));
        }

    }
}
