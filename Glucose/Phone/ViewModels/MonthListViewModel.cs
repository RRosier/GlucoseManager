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

        public ObservableCollection<KeyedList<string, MeasurementViewModel>> GroupedMeasurements
        {
            get;
            set;
        }

        public MonthListViewModel()
        {
            this.GroupedMeasurements = new ObservableCollection<KeyedList<string, MeasurementViewModel>>();
        }

        /// <summary>
        /// When override performs actions to load this views data.
        /// </summary>
        public override async Task LoadDataAsync()
        {
            var monthlyMeasurements = await StorageManager.LoadMeasurementsAsync(Month, Year);

            var groupedValues =
                from value in monthlyMeasurements
                orderby value.Model.DateTime descending
                group value by value.DayString into valuesByDay
                select new KeyedList<string, MeasurementViewModel>(valuesByDay);

            foreach (var group in groupedValues)
            {
                this.GroupedMeasurements.Add(group);
            }
        }
    }
}
