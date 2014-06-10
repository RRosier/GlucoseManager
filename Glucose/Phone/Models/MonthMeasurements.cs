using Rosier.Glucose.Phone.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Model
{
    /// <summary>
    /// Represents a single Month and all the measurements linked to it.
    /// </summary>
    public class MonthMeasurements
    {
        public int Month { get; private set; }
        public int Year { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Month" /> class.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        public MonthMeasurements(int month, int year)
        {
            this.Month = month;
            this.Year = year;
            this.Measurements = new List<Measurement>();
        }

        /// <summary>
        /// Gets or sets the measurements.
        /// </summary>
        /// <value>
        /// The measurements.
        /// </value>
        /// TODO-rro: Should we expose the measurements list or not?
        public List<Measurement> Measurements { get; set; }

        /// <summary>
        /// Loads this month's measurements from disk.
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            var loadedMeasurements = await StorageManager.LoadMeasurementsAsync(this.Month, this.Year);
            Measurements = loadedMeasurements.ToList();
        }

        /// <summary>
        /// Saves this month's measurements to disk.
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await StorageManager.SaveMeasurementsAsync(this.Measurements);
        }

        /// <summary>
        /// Adds the measurement.
        /// </summary>
        /// <param name="measurement">The measurement.</param>
        public void AddMeasurement(Measurement measurement)
        {
            this.Measurements.Add(measurement);
            this.Measurements.OrderBy(m => m.DateTime);
        }

        /// <summary>
        /// Calculates the summary for this month.
        /// </summary>
        /// <returns><see cref="MonthSummary"/> with this month's totals and averages.</returns>
        public MonthSummary CalculateSummary()
        {
            var summary = new MonthSummary()
            {
                Month = new DateTime(this.Year, this.Month, 1),
                TotalMeasures = this.Measurements.Count,
                TotalGlucose = this.Measurements.Sum(m => m.GlucoseValue),
                TotalInsuline = this.Measurements.Sum(m => m.InsulineUnits)
            };

            var dayMeasurements = from m in this.Measurements
                                  orderby m.DateTime descending
                                  group m by m.DateTime.Day into measurementsByDay
                                  select measurementsByDay;

            summary.MonthlyDayGlucoAverage = Convert.ToInt32(Math.Round(dayMeasurements.Average(d => d.Average(m => m.GlucoseValue))));
            summary.DailyAverageInsuline = Convert.ToInt32(Math.Round(dayMeasurements.Average(d => d.Sum(m => m.InsulineUnits))));

            return summary;
        }

        /// <summary>
        /// Removes the measurement.
        /// </summary>
        /// <param name="measurement">The measurement.</param>
        public void RemoveMeasurement(Measurement measurement)
        {
            this.Measurements.Remove(measurement);
        }
    }
}
