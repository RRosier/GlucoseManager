using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Model
{
    /// <summary>
    /// Represents a single glucose measurement.
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// Date and time when the value is taken.
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// The measured glucose value.
        /// </summary>
        public int GlucoseValue { get; set; }
        /// <summary>
        /// Amount of insuline units taken.
        /// </summary>
        public int InsulineUnits { get; set; }
        /// <summary>
        /// Free comments about the measurement.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>New instance of <see cref="Measurement"/> with the same data as this instance.</returns>
        internal Measurement Clone()
        {
            return new Measurement()
            {
                DateTime = this.DateTime,
                GlucoseValue = this.GlucoseValue,
                InsulineUnits = this.InsulineUnits,
                Comments = this.Comments
            };
        }
    }
}
