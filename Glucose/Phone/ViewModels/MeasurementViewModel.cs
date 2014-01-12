using Rosier.Glucose.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MeasurementViewModel : INotifyPropertyChanged
    {
        private Measurement model;

        public Measurement Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        public MeasurementViewModel(Measurement model)
        {
            this.model = model;
        }

        public MeasurementViewModel() { this.model = new Measurement(); }

        public string TimeString
        {
            get { return this.model.DateTime.ToString("HH:mm"); }
        }
        public string DayString
        {
            get { return this.model.DateTime.ToString("ddd d"); }
        }

        public int GlucoseValue 
        {
            get { return this.model.GlucoseValue; }
            set { this.model.GlucoseValue = value; NotifyPropertyChanged("GlucoseValue"); } 
        }
        /// <summary>
        /// Amount of insuline units taken.
        /// </summary>
        public int InsulineUnits 
        {
            get { return this.model.InsulineUnits; }
            set { this.model.InsulineUnits = value; NotifyPropertyChanged("InsulineUnits"); } 
        }
        /// <summary>
        /// Free comments about the measurement.
        /// </summary>
        public string Comments 
        {
            get { return this.model.Comments; }
            set { this.model.Comments = value; NotifyPropertyChanged("Comments"); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
