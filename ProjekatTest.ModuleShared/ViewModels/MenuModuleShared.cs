using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace ProjekatTest.ModuleShared.ViewModels
{
    public class MenuModuleShared: IMenuModuleShared
    {
        private IEventAggregator _evt;

        public MenuModuleShared(IEventAggregator evt)
        {
            _evt = evt;
        }

        public ICommand ButtonMenuConfigurationsClick
        {
            get
            {
                return new DelegateCommand<object>(ChangeDetailView, ChangeDetailViewCheck);
            }
        }

        private void ChangeDetailView(object context)
        {
            
        }

        private bool ChangeDetailViewCheck(object context)
        {


            return true;
        }

    }
}
