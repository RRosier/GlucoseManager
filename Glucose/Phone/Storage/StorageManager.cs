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
    }
}
