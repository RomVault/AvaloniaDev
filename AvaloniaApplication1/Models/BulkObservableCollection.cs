using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AvaloniaApplication1.Models
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> that can suspend change
    /// notifications while a large number of items are added, then raise a
    /// single <see cref="NotifyCollectionChangedAction.Reset"/> so the bound
    /// UI (e.g. a DataGrid) updates once instead of per item.
    /// </summary>
    public class BulkObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotification;

        /// <summary>
        /// Replaces the collection contents with <paramref name="items"/>,
        /// raising only a single reset notification when finished.
        /// </summary>
        public void Reset(IEnumerable<T> items)
        {
            _suppressNotification = true;
            try
            {
                Items.Clear();
                foreach (T item in items)
                {
                    Items.Add(item);
                }
            }
            finally
            {
                _suppressNotification = false;
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Adds a range of items, raising only a single reset notification.
        /// </summary>
        public void AddRange(IEnumerable<T> items)
        {
            _suppressNotification = true;
            try
            {
                foreach (T item in items)
                {
                    Items.Add(item);
                }
            }
            finally
            {
                _suppressNotification = false;
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
            {
                base.OnCollectionChanged(e);
            }
        }
    }
}
