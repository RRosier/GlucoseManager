using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class MonthSummaryObservableCollection : ObservableCollection<MonthSummaryViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthSummaryObservableCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public MonthSummaryObservableCollection(IEnumerable<MonthSummaryViewModel> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        protected override void InsertItem(int index, MonthSummaryViewModel item)
        {
            item.PropertyChanged += Item_PropertyChanged;
            base.InsertItem(index, item);
        }

        void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // TODO-rro: should we trigger a Replace instead? I suppose it's a deep copy and add it to the same index?
            // If one Person is replaced by another, that's a Replace.
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            //this.OnPropertyChanged(e);
        }

        /// <summary>
        /// Finds the item by month in the collection.
        /// </summary>
        /// <param name="dateTime">The date time representing the month.</param>
        /// <returns><see cref="MonthSummaryViewModel"/> if a corresponding item is found, else <c>null</c>.</returns>
        internal MonthSummaryViewModel FindItemByMonth(DateTime dateTime)
        {
            return (from item in this.Items
                    where item.Model.Month.Year == dateTime.Year
                    && item.Model.Month.Month == dateTime.Month
                    select item).SingleOrDefault();
        }

        /// <summary>
        /// Orders the list by mont.
        /// </summary>
        internal void OrderByMonthDesc()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    var item1 = this.ElementAt(j - 1);
                    var item2 = this.ElementAt(j);
                    if (item1.Model.Month.CompareTo(item2.Model.Month) < 0)
                    {
                        this.Remove(item1);
                        this.Insert(j, item1);
                    }
                }
            }
        }
    }
}
