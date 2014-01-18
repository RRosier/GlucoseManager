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
        public MonthListViewModel()
        {
        }

        public List<KeyedList<string, MeasurementViewModel>> GroupedMeasurements
        {
            get
            {
                var groupedValues =
                    from value in base.Measurements
                    orderby value.Model.DateTime descending
                    group value by value.DayString into valuesByDay
                    select new KeyedList<string, MeasurementViewModel>(valuesByDay);

                return new List<KeyedList<string, MeasurementViewModel>>(groupedValues);
            }
        }
    }
}
