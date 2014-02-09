using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Rosier.Glucose.Phone.Pages;

namespace Rosier.Glucose.Phone
{
    public partial class MainPanorama : MainTypedBasePage
    {
        public MainPanorama()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!ViewModel.IsDataLoaded)
            {
                ViewModel.LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddMeasurementPage.xaml", UriKind.Relative));
        }
    }
}