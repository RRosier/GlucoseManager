using Rosier.Glucose.Model;
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
        private ObservableCollection<KeyedList<string, MeasurementViewModel>> groupedMeasurements = null;

        public MonthListViewModel()
        {
        }

        public bool SetReload { get; set; }

        public ObservableCollection<KeyedList<string, MeasurementViewModel>> GroupedMeasurements
        {
            get
            {
                if (this.groupedMeasurements == null || SetReload)
                {
                    var groupedValues =
                        from value in base.Measurements
                        orderby value.Model.DateTime descending
                        group value by value.DayString into valuesByDay
                        select new KeyedList<string, MeasurementViewModel>(valuesByDay);
                    this.groupedMeasurements = new ObservableCollection<KeyedList<string, MeasurementViewModel>>(groupedValues);
                    SetReload = false;
                }

                return this.groupedMeasurements;
            }
        }
    }
}
