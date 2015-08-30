using Microsoft.Practices.Prism.Mvvm;
using ProjekatTest.Infrastructure.Interfaces;
using ProjekatTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using ProjekatTest.Infrastructure.Events;

namespace ProjekatTestModule.ViewModels
{
    public class MainViewModel: BindableBase, IMainViewModel
    {
        public IPersonService personService;
        public IEventAggregator evt;


        public ObservableCollection<Testtable> Users
        {
            get;
            private set;
        }

        private Testtable _selectedItem ;
        public Testtable SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                
                SetProperty(ref _selectedItem, value);
                evt.GetEvent<SelectedListEvent>().Publish(_selectedItem);
            }
        }

        public void AddUsers(ICollection<Testtable> list)
        {
            this.Users.Clear();
            foreach( var person in list)
            {  
                this.Users.Add(person);
            }
        }

        public MainViewModel(IPersonService personService, IEventAggregator evt)
        {
            this.Users = new ObservableCollection<Testtable>();
            this.personService = personService;
            this.evt = evt;

            AddUsers(personService.GetAllPersons());
        }

    }
}
