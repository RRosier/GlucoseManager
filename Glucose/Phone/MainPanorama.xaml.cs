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
using Rosier.Glucose.Phone.ViewModels;

namespace Rosier.Glucose.Phone
{
    public partial class MainPanorama : MainTypedBasePage
    {
        public MainPanorama()
            : base()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!ViewModel.IsDataLoaded)
            {
                await ViewModel.LoadDataAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddMeasurementPage.xaml", UriKind.Relative));
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (monthlySummaryList.SelectedItem == null)
                return;

            var selectedItem = (monthlySummaryList.SelectedItem as MonthSummaryViewModel);
            var uri = string.Format("/MonthListPage.xaml?month={0}&year={1}", selectedItem.Model.Month.Month, selectedItem.Model.Month.Year);

            // Navigate to the new page
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));

            // Reset selected item to null (no selection)
            monthlySummaryList.SelectedItem = null;
        }
    }
}