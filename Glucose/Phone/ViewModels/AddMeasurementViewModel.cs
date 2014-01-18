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

        public void AddMeasurement(MeasurementViewModel measurementViewModel)
        {
            base.Measurements.Add(this.Measurement);
        }
    }
}
