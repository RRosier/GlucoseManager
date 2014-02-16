using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Rosier.Glucose.Phone.Resources;
using Rosier.Glucose.Phone.ViewModels;
using Rosier.Glucose.Phone.Pages;

namespace Rosier.Glucose.Phone
{
    public partial class MonthListPage : MonthListTypedBasePage
    {
        // Constructor
        public MonthListPage()
            : base()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            //var vm = new MonthListViewModel();
            //DataContext = vm;// App.ViewModel;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            int month;
            int year;

            this.GetQueryStringParameters(out month, out year);

            this.ViewModel.Month = month;
            this.ViewModel.Year = year;
            await this.ViewModel.LoadDataAsync();

            // clear collection so that it gets rebound with new data
            //ViewModel.GroupedMeasurements = null;
            //DataContext = ViewModel;
            //if (!App.ViewModel.IsDataLoaded)
            //{
            //    App.ViewModel.LoadData();
            //}
        }

        private void GetQueryStringParameters(out int month, out int year)
        {
            string monthString, yearString;

            if (!NavigationContext.QueryString.TryGetValue("year", out yearString))
            {
                throw new ArgumentException("Year not present.");
            }

            if (!NavigationContext.QueryString.TryGetValue("month", out monthString))
            {
                throw new ArgumentException("Month not present.");
            }

            // TODO-rro: perform more date validations on the input parameters.
            if (!Int32.TryParse(monthString, out month))
            {
                throw new InvalidCastException("Month is not valid.");
            }

            if (!Int32.TryParse(yearString, out year))
            {
                throw new InvalidCastException("Year is not valid.");
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddMeasurementPage.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}