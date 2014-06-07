using System.Threading.Tasks;

using Rosier.Glucose.Model;

namespace Rosier.Glucose.Phone.ViewModels
{
    /// <summary>
    /// View model for the AddMeasurement page
    /// </summary>
    public class AddMeasurementViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        public MeasurementViewModel Measurement
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementViewModel"/> class.
        /// </summary>
        public AddMeasurementViewModel()
        {
            this.Measurement = new MeasurementViewModel();
        }

        /// <summary>
        /// Updates the storage with the newly added measurement.
        /// </summary>
        /// <param name="measurementViewModel">The measurement view model.</param>
        /// <returns></returns>
        public async Task AddMeasurement(MeasurementViewModel measurementViewModel)
        {
            var newMeasurement = measurementViewModel.Model;
            var month = new Month(newMeasurement.DateTime.Month, newMeasurement.DateTime.Year);
            await month.Load();
            month.AddMeasurement(newMeasurement);
            var monthSummary = month.CalculateSummary();
            await month.Save();
            await App.UpdateSummary(monthSummary);
        }
    }
}
