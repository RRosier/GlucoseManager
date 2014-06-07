using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rosier.Glucose.Model;
using Rosier.Glucose.Phone.Storage;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class AddMeasurementViewModel : ViewModelBase
    {
        public MeasurementViewModel Measurement
        {
            get;
            set;
        }

        public AddMeasurementViewModel()
        {
            this.Measurement = new MeasurementViewModel();
        }

        public async Task AddMeasurement(MeasurementViewModel measurementViewModel)
        {
            ////List<MeasurementViewModel> list = new List<MeasurementViewModel>();

            var newMeasurement = measurementViewModel.Model;
            var month = new Month(newMeasurement.DateTime.Month, newMeasurement.DateTime.Year);
            await month.Load();
            month.AddMeasurement(newMeasurement);
            var monthSummary = month.CalculateSummary();
            await month.Save();
            await App.UpdateSummary(monthSummary);



            ////var monthMeasurements = await StorageManager.LoadMeasurementsAsync(measurementViewModel.Model.DateTime.Month, measurementViewModel.Model.DateTime.Year);
            ////if (monthMeasurements != null)
            ////    list.AddRange(monthMeasurements);

            ////list.Add(measurementViewModel);

            ////await StorageManager.SaveMeasurementsAsync(list.Select(m => m.Model));

            ////// calculate daily average
            ////var dailyAverages = from measurement in list
            ////                    group measurement by measurement.DayString into day
            ////                    select
            ////                        new
            ////                        {
            ////                            Day = day.Key,
            ////                            AverageInsuline = day.Average(d => d.InsulineUnits),
            ////                            AverageGlucose = day.Average(d => d.GlucoseValue)
            ////                        };

            ////var monthlyAverage = new MonthSummary(){ }



            ////await this.UpdateSummary();
        }

        ////private async Task UpdateSummary()
        ////{
        ////    await App.UpdateSummary(this.Measurement.Model);
        ////}
    }
}
