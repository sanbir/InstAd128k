using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Extensions
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable coll)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in coll)
            {
                collection.Add((T)item);
            }
            return collection;
        }
    }
}
