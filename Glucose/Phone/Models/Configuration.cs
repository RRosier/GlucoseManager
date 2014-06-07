using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage.Streams;

namespace Rosier.Glucose.Phone.Models
{
    /// <summary>
    /// Represents read-only application configuration settings.
    /// </summary>
    public class Configuration
    {
        private const string ConfigurationFile = "/Config/configuration.config";

        public string LiveApiClientId { get; set; }

        public static async Task<Configuration> Load()
        {
            var package = Windows.ApplicationModel.Package.Current;
            var installedLocation = package.InstalledLocation;
            var configFile = await installedLocation.GetFileAsync(ConfigurationFile);

            using (var fileStream = await configFile.OpenStreamForReadAsync())
            {
                var serializer = new XmlSerializer(typeof(Configuration));
                return (Configuration)serializer.Deserialize(fileStream);
            }
        }
    }
}
