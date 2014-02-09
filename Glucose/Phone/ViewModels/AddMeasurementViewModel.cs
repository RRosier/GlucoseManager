using Rosier.Glucose.Phone.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await StorageManager.SaveMeasurementsAsync(new[] { Measurement.Model });

            await this.UpdateSummary();


            // only add to view model when month corresponds
            base.Measurements.Add(this.Measurement);
        }

        private async Task UpdateSummary()
        {
            var month = Measurement.Date.ToString("MM-yyyy");

            var monthSummary = App.SummaryData.Where(sd => sd.Month == month).SingleOrDefault();
            if (monthSummary != null)
            {
                monthSummary.TotalMeasures++;
                monthSummary.TotalGlucose += Measurement.GlucoseValue;
                monthSummary.TotalInsuline += Measurement.InsulineUnits;
            }
            else
            {
                App.SummaryData.Add(new Model.MonthSummary { Month = month, TotalMeasures = 1, TotalGlucose = Measurement.GlucoseValue, TotalInsuline = Measurement.InsulineUnits });
            }

            await App.SaveSummaryDataAsync();
        }
    }
}
