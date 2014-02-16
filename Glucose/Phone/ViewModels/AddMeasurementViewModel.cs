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
            List<MeasurementViewModel> list = new List<MeasurementViewModel>();
            var monthMeasurements = await StorageManager.LoadMeasurementsAsync(measurementViewModel.Model.DateTime.Month, measurementViewModel.Model.DateTime.Year);
            if (monthMeasurements != null)
                list.AddRange(monthMeasurements);

            list.Add(measurementViewModel);

            await StorageManager.SaveMeasurementsAsync(list.Select(m => m.Model));

            await this.UpdateSummary();
        }

        private async Task UpdateSummary()
        {
            await App.UpdateSummary(this.Measurement.Model);
        }
    }
}
