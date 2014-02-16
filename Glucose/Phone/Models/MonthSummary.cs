using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Model
{
    /// <summary>
    /// Summary class for a single month.
    /// </summary>
    public class MonthSummary
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        /// <remarks>Represented as the first of each month.</remarks>
        public DateTime Month { get; set; }
        /// <summary>
        /// Gets or sets the total measures for this month.
        /// </summary>
        /// <value>
        /// The total measures.
        /// </value>
        public int TotalMeasures { get; set; }
        /// <summary>
        /// Gets or sets the total insuline taken for this month.
        /// </summary>
        /// <value>
        /// The total insuline.
        /// </value>
        public int TotalInsuline { get; set; }
        /// <summary>
        /// Gets or sets the total glucose measured for this month.
        /// </summary>
        /// <value>
        /// The total glucose.
        /// </value>
        public int TotalGlucose { get; set; }
    }
}
