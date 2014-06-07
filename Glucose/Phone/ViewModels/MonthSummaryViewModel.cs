using Rosier.Glucose.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Glucose.Phone.ViewModels
{
    /// <summary>
    /// View model to bind a summary per month to.
    /// </summary>
    public class MonthSummaryViewModel : ViewModelBase
    {
        private MonthSummary model;

        public MonthSummary Model
        {
            get { return this.model; }
            set { this.model = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthSummaryViewModel"/> class.
        /// </summary>
        /// <param name="model">The underlying model.</param>
        public MonthSummaryViewModel(MonthSummary model)
        {
            this.model = model;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthSummaryViewModel"/> class.
        /// </summary>
        /// <param name="model">The first measurement for this month.</param>
        public MonthSummaryViewModel(Measurement measurement)
        {
            this.model = new MonthSummary();
            this.model.Month = new DateTime(measurement.DateTime.Year, measurement.DateTime.Month, 1);
            this.model.TotalMeasures = 1;
            this.model.TotalGlucose = measurement.GlucoseValue;
            this.model.TotalInsuline = measurement.InsulineUnits;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthSummaryViewModel"/> class.
        /// </summary>
        public MonthSummaryViewModel()
        {
            this.model = new MonthSummary();
        }

        /// <summary>
        /// Gets month in string fromat to display.
        /// </summary>
        /// <value>
        /// The month as display string.
        /// </value>
        public string DisplayMonth
        {
            get { return this.model.Month.ToString("MMM yyyy"); }
        }

        /// <summary>
        /// Gets the average glucose measurement for this month.
        /// </summary>
        /// <value>
        /// The average glucose measurement.
        /// </value>
        public int Glucose
        {
            get { return this.model.MonthlyDayGlucoAverage; }
        }

        /// <summary>
        /// Gets the average insuline intake for this month.
        /// </summary>
        /// <value>
        /// The average insuline intake.
        /// </value>
        public int Insuline
        {
            get { return this.model.DailyAverageInsuline; }
        }

        /// <summary>
        /// Adds the measurement to the monthly summary and notify property changed.
        /// </summary>
        /// <param name="measurement">The measurement.</param>
        public void AddMeasurement(Measurement measurement)
        {
            this.model.TotalMeasures++;
            this.model.TotalGlucose += measurement.GlucoseValue;
            this.model.TotalInsuline += measurement.InsulineUnits;
            NotifyPropertyChanged("Glucose");
            NotifyPropertyChanged("Insuline");
        }
    }
}
