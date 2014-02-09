using Rosier.Glucose.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Glucose.Phone.ViewModels
{
    /// <summary>
    /// View model to bind a summary per month to.
    /// </summary>
    public class MonthSummaryViewModel : ViewModelBase
    {
        private MonthSummary model;

        public MonthSummary Model
        {
            get { return this.model; }
            set { this.model = value; NotifyPropertyChanged(); }
        }

        public MonthSummaryViewModel(MonthSummary model)
        {
            this.model = model;
        }

        public MonthSummaryViewModel()
        {
            this.model = new MonthSummary();
        }

        public string DisplayMonth
        {
            get { return this.model.Month; }
        }

        public int Glucose
        {
            get { return this.model.AverageGlucose; }
        }

        public int Insuline
        {
            get { return this.model.AverageInsuline; }
        }
    }
}
