using Newtonsoft.Json;
using Rosier.Glucose.Model;
using Rosier.Glucose.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.Storage
{
    /// <summary>
    /// Class to handle storage operations.
    /// </summary>
    internal class StorageManager
    {
        private const string MonthFolder = "measurements";
        private const string SummaryFile = "summary.json";

        /// <summary>
        /// Loads the measurements for the provided month asynchronous.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>List of <see cref="Measurements"/> for the provided month.</returns>
        public async static Task<IEnumerable<Measurement>> LoadMeasurementsAsync(int month, int year)
        {
            var monthString = CreateMonthFileName(month, year);
            var measurements = new List<Measurement>();
            var filepath = Path.Combine(MonthFolder, monthString) + ".json";
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            if (isolatedStorageFile.FileExists(filepath))
            {
                var fileStream = isolatedStorageFile.OpenFile(filepath, FileMode.Open);
                using (var reader = new StreamReader(fileStream))
                {
                    var fileContent = await reader.ReadToEndAsync();

                    measurements = JsonConvert.DeserializeObject<List<Measurement>>(fileContent);
                }
            }

            return measurements;
        }

        /// <summary>
        /// Saves the measurements asynchronous to storage.
        /// </summary>
        /// <param name="measurements">The measurements.</param>
        /// <returns></returns>
        public static async Task SaveMeasurementsAsync(IEnumerable<Measurement> measurements)
        {
            var tasks = new List<Task>();
            var monthMeasurements = from m in measurements
                                    group m by m.DateTime.ToString("MM-yyyy") into month
                                    select new { Month = month.Key, Measurements = month };
            // TODO-rro: Don't use date format hard coded, use same method created for it
            await Task.WhenAll(monthMeasurements.Select(mm => SaveMonthMeasurementsAsync(mm.Measurements, mm.Month)).ToArray());
        }

        private async static Task SaveMonthMeasurementsAsync(IEnumerable<Measurement> measurements, string month)
        {
            // TODO-rro: Don't use date format hard coded, use same method created for it
            var filepath = Path.Combine(MonthFolder, month) + ".json";

            var serializedString = JsonConvert.SerializeObject(measurements);
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            VerifyFolderExistsAsync(isolatedStorageFile);

            using (var fileStream = isolatedStorageFile.OpenFile(filepath, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(fileStream))
            {
                await writer.WriteAsync(serializedString);
            }
        }

        private static void VerifyFolderExistsAsync(IsolatedStorageFile isolatedStorageFile)
        {
            if (!isolatedStorageFile.DirectoryExists(MonthFolder))
            {
                isolatedStorageFile.CreateDirectory(MonthFolder);
            }
        }

        /// <summary>
        /// Reads the summary data asynchronous from the local storage.
        /// </summary>
        /// <returns>Enumerable of the stored summary data.</returns>
        internal static async Task<IEnumerable<MonthSummary>> ReadSummaryDataAsync()
        {
            var data = new List<MonthSummary>();
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            if (isolatedStorageFile.FileExists(SummaryFile))
            {
                var fileStream = isolatedStorageFile.OpenFile(SummaryFile, FileMode.Open);
                using (var reader = new StreamReader(fileStream))
                {
                    var fileContent = await reader.ReadToEndAsync();
                    data = JsonConvert.DeserializeObject<List<MonthSummary>>(fileContent);
                }
            }

            return data;
        }

        /// <summary>
        /// Writes the summary data asynchronous to the local storage.
        /// </summary>
        /// <returns>
        /// <see cref="Task"/> object.
        /// </returns>
        internal static async Task WriteSummaryDataAsync(IEnumerable<MonthSummary> data)
        {
            var serializedString = JsonConvert.SerializeObject(data);
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = isolatedStorageFile.OpenFile(SummaryFile, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(fileStream))
            {
                await writer.WriteAsync(serializedString);
            }
        }

        private static string CreateMonthFileName(int month, int year)
        {
            return string.Format("{0}-{1}", month.ToString("00"), year);
        }
    }
}
