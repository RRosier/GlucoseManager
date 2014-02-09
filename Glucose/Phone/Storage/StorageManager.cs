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
    public class StorageManager
    {
        private const string MonthFolder = "measurements";
        private const string SummaryFile = "summary.json";

        public async static Task<IEnumerable<MeasurementViewModel>> LoadMeasurementsAsync(string month)
        {
            var measurements = new List<MeasurementViewModel>();
            var filepath = Path.Combine(MonthFolder, month) + ".json";
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();


            if (isolatedStorageFile.FileExists(filepath))
            {
                await Task.Factory.StartNew(async () =>
                {
                    var fileStream = isolatedStorageFile.OpenFile(filepath, FileMode.Open);
                    using (var reader = new StreamReader(fileStream))
                    {
                        var fileContent = await reader.ReadToEndAsync();
                        var models = await JsonConvert.DeserializeObjectAsync<List<Measurement>>(fileContent);

                        foreach (var m in models)
                        {
                            measurements.Add(new MeasurementViewModel(m));
                        }
                    }

                });
            }


            return measurements;
        }

        public static async Task SaveMeasurementsAsync(IEnumerable<Measurement> measurements)
        {
            var tasks = new List<Task>();
            var monthMeasurements = from m in measurements
                                    group m by m.DateTime.ToString("MM-yyyy") into month
                                    select new { Month = month.Key, Measurements = month };

            await Task.WhenAll(monthMeasurements.Select(mm => SaveMonthMeasurementsAsync(mm.Measurements, mm.Month)).ToArray());

            //foreach (var mm in monthMeasurements)
            //{
            //    tasks.Add(SaveMonthMeasurementsAsync(mm.Measurements, mm.Month));
            //}

            //return Task.WhenAll(tasks);// tasks.ToArray();// Task.WaitAll(tasks.ToArray());
        }

        private async static Task SaveMonthMeasurementsAsync(IEnumerable<Measurement> measurements, string month)
        {
            var filepath = Path.Combine(MonthFolder, month) + ".json";

            var serializedString = await JsonConvert.SerializeObjectAsync(measurements);
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            VerifyFolderExistsAsync(isolatedStorageFile);

            using (var fileStream = isolatedStorageFile.OpenFile(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
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
                await Task.Factory.StartNew(async () =>
                {
                    var fileStream = isolatedStorageFile.OpenFile(SummaryFile, FileMode.Open);
                    using (var reader = new StreamReader(fileStream))
                    {
                        var fileContent = await reader.ReadToEndAsync();
                        data = await JsonConvert.DeserializeObjectAsync<List<MonthSummary>>(fileContent);
                    }
                });
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
            var serializedString = await JsonConvert.SerializeObjectAsync(data);
            var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = isolatedStorageFile.OpenFile(SummaryFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(fileStream))
            {
                await writer.WriteAsync(serializedString);
            }
        }
    }
}
