using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Rosier.Glucose.Phone.Resources;
using Rosier.Glucose.Phone.Storage;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        public ObservableCollection<MonthSummaryViewModel> Summary { get; set; }

        public int CurrentMonthAverageGlucose
        {
            get
            {
                if (this.Summary.Count == 0)
                    return 0;

                return this.Summary.First().Glucose;
            }
        }

        public int CurrentMonthAverageInsuline
        {
            get
            {
                if (this.Summary.Count == 0)
                    return 0;

                return this.Summary.First().Insuline;
            }
        }

        public override async void LoadData()
        {
            if (this.Summary == null)
            {
                this.Summary = new ObservableCollection<MonthSummaryViewModel>();
            }

            this.Summary.Clear();

            if (App.SummaryData == null)
            {
                await App.LoadSummaryDataAsync();
                // TODO: pass in the event
                App.SummaryData.CollectionChanged += SummaryData_CollectionChanged;
            }

            foreach (var s in App.SummaryData)
            {
                this.Summary.Add(new MonthSummaryViewModel(s));
            }

            this.IsDataLoaded = true;
        }

        void SummaryData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.LoadData();
            //NotifyPropertyChanged("Summary");
        }
    }
}