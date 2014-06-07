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
    public class Month
    {
        private int month;
        private int year;

        /// <summary>
        /// Initializes a new instance of the <see cref="Month" /> class.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        public Month(int month, int year)
        {
            this.month = month;
            this.year = year;
            this.Measurements = new List<Measurement>();
        }

        private List<Measurement> Measurements { get; set; }

        /// <summary>
        /// Loads this month's measurements from disk.
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            var loadedMeasurements = await StorageManager.LoadMeasurementsAsync(month, year);
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
                Month = new DateTime(this.year, this.month, 1),
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
    }
}
