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
            await App.UpdateSummary(this.Measurement.Model);
        }
    }
}
