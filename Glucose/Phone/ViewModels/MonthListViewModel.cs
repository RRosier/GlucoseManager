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
        public ObservableCollection<MeasurementViewModel> Measurements { get; set; }

        public MonthListViewModel()
        {
            this.Measurements = new ObservableCollection<MeasurementViewModel>();
        }

        public List<KeyedList<string, MeasurementViewModel>> GroupedMeasurements
        {
            get
            {
                var groupedValues =
                    from value in this.Measurements
                    orderby value.Model.DateTime descending
                    group value by value.DayString into valuesByDay
                    select new KeyedList<string, MeasurementViewModel>(valuesByDay);

                return new List<KeyedList<string, MeasurementViewModel>>(groupedValues);
            }
        }

        public void LoadData()
        {
            // Sample data; replace with real data
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 12, 10, 12, 0), GlucoseValue=129, InsulineUnits=15,Comments=""}));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 12, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 12, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 12, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 12, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 13, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 13, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 13, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 13, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 11, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 11, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 11, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 10, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 10, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 10, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 9, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 9, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 9, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
            this.Measurements.Add(new MeasurementViewModel(new Measurement() { DateTime = new DateTime(2014, 1, 9, 10, 12, 0), GlucoseValue = 129, InsulineUnits = 15, Comments = "" }));
        }
    }
}
