using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Extensions
{
    public static class IEnumHelper
    {
        public static void Add<T>(IEnumerable<T> first, T second)
        {
            first = first.Concat(new[] { second });
        }

        public static void Add<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            first = first.Concat(second);
        }

        public static void Remove<T>(IEnumerable<T> from, T what)
        {
            from = from.Except(new[] { what });
        }

        public static void Remove<T>(IEnumerable<T> from, IEnumerable<T> what)
        {
            from = from.Except(what);
        }
    }
}
