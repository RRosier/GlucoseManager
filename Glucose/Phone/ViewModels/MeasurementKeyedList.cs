using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MeasurementKeyedList : KeyedList<string, MeasurementViewModel>
    {
        public MeasurementKeyedList() { }

        public MeasurementKeyedList(string key, List<MeasurementViewModel> items)
            : base(key, items)
        {
        }

        public MeasurementKeyedList(IGrouping<string, MeasurementViewModel> grouping)
            : base(grouping)
        {
        }

        new string Key { get; set; }
    }
}
