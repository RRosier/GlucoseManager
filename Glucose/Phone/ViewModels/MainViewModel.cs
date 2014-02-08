using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Rosier.Glucose.Phone.Resources;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        public ObservableCollection<MonthSummaryViewModel> Summary { get; set; }

        public int CurrentMonthAverageGlucose { get { return 120; } }
        public int CurrentMonthAverageInsuline { get { return 12; } }
    }
}