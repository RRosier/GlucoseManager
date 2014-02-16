using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Rosier.Glucose.Phone.Resources;
using Rosier.Glucose.Phone.Storage;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        public MonthSummaryObservableCollection Summary { get; set; }

        public int CurrentMonthAverageGlucose
        {
            get
            {
                if (this.Summary == null || this.Summary.Count == 0)
                    return 0;

                return this.Summary.First().Glucose;
            }
        }

        public int CurrentMonthAverageInsuline
        {
            get
            {
                if (this.Summary == null || this.Summary.Count == 0)
                    return 0;

                return this.Summary.First().Insuline;
            }
        }

        public override async Task LoadDataAsync()
        {
            if (this.Summary == null)
            {
                await App.LoadSummaryDataAsync();
                this.Summary = App.SummaryData;
                // TODO: pass in the event
                App.SummaryData.CollectionChanged += SummaryData_CollectionChanged;
                NotifyPropertyChanged("CurrentMonthAverageGlucose");
                NotifyPropertyChanged("CurrentMonthAverageInsuline");
                NotifyPropertyChanged("Summary");
            }

            this.IsDataLoaded = true;
        }

        void SummaryData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //this.LoadData();
            NotifyPropertyChanged("CurrentMonthAverageGlucose");
            NotifyPropertyChanged("CurrentMonthAverageInsuline");
            NotifyPropertyChanged("Summary");
        }
    }
}