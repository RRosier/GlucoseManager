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
        /// When override performs actions to load this views data.
        /// </summary>
        /// <returns></returns>
        public override Task LoadDataAsync()
        {
            //return Task.Factory.StartNew(() =>
            //{
            if (App.SelectedMeasurement != null)
            {
                //// request update of this entity
                this.Measurement = new MeasurementViewModel(App.SelectedMeasurement.Clone());
                this.NotifyPropertyChanged("Measurement");
            }
            return Task.FromResult<bool>(true);
        }

        /// <summary>
        /// Updates the storage with the newly added measurement.
        /// </summary>
        /// <param name="measurementViewModel">The measurement view model.</param>
        /// <returns></returns>
        public async Task AddMeasurement(MeasurementViewModel measurementViewModel)
        {
            var newMeasurement = measurementViewModel.Model;
            MonthMeasurements month;
            if (App.SelectedMeasurement == null)
            {
                month = new MonthMeasurements(newMeasurement.DateTime.Month, newMeasurement.DateTime.Year);
                await month.Load();
            }
            else
            {
                // Update, no need to load the list again since it's already loaded.
                month = App.SelectedMonth;
                month.RemoveMeasurement(App.SelectedMeasurement);
            }

            month.AddMeasurement(newMeasurement);
            var monthSummary = month.CalculateSummary();
            await month.Save();
            await App.UpdateSummary(monthSummary);
        }
    }
}
