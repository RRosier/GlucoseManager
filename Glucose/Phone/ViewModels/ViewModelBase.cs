﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rosier.Glucose.Phone.ViewModels
{
    /// <summary>
    /// Base class for all ViewModels.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private ObservableCollection<MeasurementViewModel> measurements = new ObservableCollection<MeasurementViewModel>();

        public ViewModelBase()
        {
            this.Measurements.CollectionChanged += Measurements_CollectionChanged;
        }

        /// <summary>
        /// Gets the current <see cref="Application"/> instance.
        /// </summary>
        /// <value>
        /// The current <see cref="Application"/> instance.
        /// </value>
        protected App App
        {
            get { return (App)Application.Current; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether data of this ViewModel is loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if data is loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsDataLoaded
        {
            get;
            protected set;
        }

        /// <summary>
        /// When override performs actions to load this views data.
        /// </summary>
        public virtual void LoadData() { }

        protected virtual void Measurements_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// Gets the month measurements.
        /// </summary>
        /// <value>
        /// The month measurements.
        /// </value>
        public ObservableCollection<MeasurementViewModel> Measurements
        {
            get
            {
                if (!DesignerProperties.IsInDesignTool)
                    this.measurements = ((App)Application.Current).Measurements;
                return this.measurements;
            }
            set
            {
                this.measurements = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("A property name is required", propertyName);
            }

            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
