using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Rosier.Glucose.Phone.ViewModels;

namespace Rosier.Glucose.Phone
{
    public partial class AddMeasurementPage : PhoneApplicationPage
    {
        public AddMeasurementPage()
        {
            InitializeComponent();
            DataContext = new MeasurementViewModel();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            var model = (DataContext as MeasurementViewModel).Model;
        }
    }
}