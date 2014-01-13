using Rosier.Glucose.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MeasurementViewModel : ViewModelBase
    {
        private Measurement model;

        public Measurement Model
        {
            get { return this.model; }
            set { this.model = value; NotifyPropertyChanged(); }
        }

        public MeasurementViewModel(Measurement model)
        {
            this.model = model;
        }

        public MeasurementViewModel() 
        { 
            this.model = new Measurement();
            this.model.DateTime = DateTime.Now;
        }

        public string TimeString
        {
            get { return this.model.DateTime.ToString("HH:mm"); }
        }
        public string DayString
        {
            get { return this.model.DateTime.ToString("ddd d"); }
        }

        public DateTime Date
        {
            get { return this.model.DateTime; }
            set
            {
                this.model.DateTime = value.Date.Add(this.model.DateTime.TimeOfDay);
                NotifyPropertyChanged();
            }
        }

        public DateTime Time
        {
            get { return this.model.DateTime; }
            set
            {
                this.model.DateTime = this.model.DateTime.Date.Add(value.TimeOfDay);
                NotifyPropertyChanged();
            }
        }

        public int GlucoseValue 
        {
            get { return this.model.GlucoseValue; }
            set { this.model.GlucoseValue = value; NotifyPropertyChanged(); } 
        }
        /// <summary>
        /// Amount of insuline units taken.
        /// </summary>
        public int InsulineUnits 
        {
            get { return this.model.InsulineUnits; }
            set { this.model.InsulineUnits = value; NotifyPropertyChanged(); } 
        }
        /// <summary>
        /// Free comments about the measurement.
        /// </summary>
        public string Comments 
        {
            get { return this.model.Comments; }
            set { this.model.Comments = value; NotifyPropertyChanged(); } 
        }
    }
}
