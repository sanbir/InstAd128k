using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ChangeCharOnIndexTo(this string str, int index, string toStr)
        {
            if (index == 0)
            {
                return toStr + str.Substring(index + 1);
            }

            if (index == str.Length - 1)
            {
                return str.Substring(0, str.Length - 1) + toStr;
            }

            return str.Substring(0, index) + toStr + str.Substring(index + 1);
        }

        public static string ChangeCharOnIndexTo(this string str, int index, char toStr)
        {
            if (index == 0)
            {
                return toStr + str.Substring(index + 1);
            }

            if(index == str.Length-1)
            {
                return str.Substring(0, str.Length - 1) + toStr;
            }

            return str.Substring(0, index) + toStr + str.Substring(index + 1);
        }

        public static string NormalizeIt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            return str.ToLower().Trim();
        }
    }
}
