﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjekatTest.ModuleConfigurations.ViewModels;

namespace ProjekatTest.ModuleConfigurations.Views
{
    /// <summary>
    /// Interaction logic for ModuleConfigurationsDetail.xaml
    /// </summary>
    public partial class ModuleConfigurationsDetail : UserControl
    {
        public ModuleConfigurationsDetail(DetailViewModelModuleConfigurations viewM)
        {
            this.DataContext = viewM;

            InitializeComponent();
        }
    }
}
