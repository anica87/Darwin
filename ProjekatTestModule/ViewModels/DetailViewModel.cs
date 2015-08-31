using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using ProjekatTest.Infrastructure.Events;
using ProjekatTest.Infrastructure.Interfaces;
using ProjekatTest.Infrastructure.Models;
using ProjekatTest.Services.Services;
using Shared.BusinessObjects.Devices;
using System.ComponentModel;
using ProjekatTest.Infrastructure.Interfaces.Services;

namespace ProjekatTestModule.ViewModels
{
    public class DetailViewModel: BindableBase, IDetailViewModel
    {
        private readonly IEventAggregator evt;
        private readonly IPersonService personService;

        private DeviceType typeOfDevice;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public IEnumerable<DeviceType> AllDeviceTypes {

            get {
                return Enum.GetValues(typeof(DeviceType)).Cast<DeviceType>();
            }
        }

        public DeviceType TypeOfDevice
        {
            get { return typeOfDevice; }
            set
            {
                if (typeOfDevice != value)
                {
                    typeOfDevice = value;
                    RaisePropertyChanged("TypeOfDevice");
                }
            }

        }

        protected void RaisePropertyChanged(string p)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(p));
            }
        }

        public ObservableCollection<Testtable2> ListaTipova { get; set; } 

        private string _nname;
        public string NName { get { return _nname; }
                 set
            { SetProperty(ref _nname, value); }
        }

        private Testtable _selected;
        public Testtable Selected
        {
            get { return _selected; }
            set
            { SetProperty(ref _selected, value); }
        }

        public DetailViewModel(IEventAggregator evt, IPersonService personService)
        {
            this.evt = evt;
            this.personService = personService;

            ListaTipova = new ObservableCollection<Testtable2>( personService.GetAllPersonTypes());

            evt.GetEvent<SelectedListEvent>().Subscribe(AddNewSelected);
        }

        public void AddNewSelected(Object obj)
        {
            if (obj != null)
            {
                Selected = (Testtable)obj;
                //Selected = personService.GetPersonById(((Testtable) obj).Id);

            }

                   
         
        }
        public Testtable SaveNewSelected()
        {
            return Selected;
        }

    }
}
