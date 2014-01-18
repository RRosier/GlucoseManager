using Microsoft.Phone.Controls;
using Rosier.Glucose.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.Pages
{
    /// <summary>
    /// Base class for all pages.
    /// </summary>
    public class BasePage<T> : PhoneApplicationPage
        where T : ViewModelBase, new()
    {

        public BasePage()
        {
            this.ViewModel = new T();
            DataContext = this.ViewModel;
        }

        /// <summary>
        /// Gets the curret application object.
        /// </summary>
        /// <value>
        /// The curret application.
        /// </value>
        public App CurretApp
        {
            get
            {
                return (App)App.Current;
            }
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public T ViewModel
        {
            get;
            set;
        }
    }
}
