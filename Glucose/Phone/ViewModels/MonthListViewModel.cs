using Rosier.Glucose.Model;
using Rosier.Glucose.Phone.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MonthListViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        public string DisplayMonth { get { return new DateTime(this.Year, this.Month, 1).ToString("MMMM yyyy"); } }

        public virtual MonthlySummaryGroupedList GroupedMeasurements
        {
            get;
            set;
        }

        public MonthListViewModel()
        {
            this.GroupedMeasurements = new MonthlySummaryGroupedList();
        }

        /// <summary>
        /// When override performs actions to load this views data.
        /// </summary>
        public override async Task LoadDataAsync()
        {
            if (App.SelectedMonth == null || App.SelectedMonth.Month != this.Month || App.SelectedMonth.Year != this.Year)
            {
                //// load new measurements for selected month, else use previous loaded data
                App.SelectedMonth = new MonthMeasurements(this.Month, this.Year);
                await App.SelectedMonth.Load();
            }

            var viewModels = App.SelectedMonth.Measurements.Select(m => new MeasurementViewModel(m));

            var groupedValues =
                from value in viewModels
                orderby value.Model.DateTime descending
                group value by value.DayString into valuesByDay
                select new MeasurementKeyedList(valuesByDay);

            foreach (var group in groupedValues)
            {
                this.GroupedMeasurements.Add(group);
            }
        }

        /// <summary>
        /// Clears the data.
        /// </summary>
        public void ClearData()
        {
            App.SelectedMonth = null;
            App.SelectedMeasurement = null;
        }
    }
}
