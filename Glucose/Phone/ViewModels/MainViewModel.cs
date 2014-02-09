using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Rosier.Glucose.Phone.Resources;
using Rosier.Glucose.Phone.Storage;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.Summary = new ObservableCollection<MonthSummaryViewModel>();
        }

        public ObservableCollection<MonthSummaryViewModel> Summary { get; set; }

        public int CurrentMonthAverageGlucose { get { return 120; } }
        public int CurrentMonthAverageInsuline { get { return 12; } }

        public override async void LoadData()
        {
            this.Summary.Clear();

            if (App.SummaryData == null)
            {
                await App.LoadSummaryDataAsync();
            }

            foreach (var s in App.SummaryData)
            {
                this.Summary.Add(new MonthSummaryViewModel(s));
            }

            this.IsDataLoaded = true;
        }
    }
}