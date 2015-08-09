using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Helpers
{
    public static class LevenshteinDistance
    {
        public static int Find(string string1, string string2)
        {
            if (string1 == null) throw new ArgumentNullException("string1");
            if (string2 == null) throw new ArgumentNullException("string2");
            var m = new int[string1.Length + 1, string2.Length + 1];

            for (int i = 0; i <= string1.Length; i++) m[i, 0] = i;
            for (int j = 0; j <= string2.Length; j++) m[0, j] = j;

            for (int i = 1; i <= string1.Length; i++)
                for (int j = 1; j <= string2.Length; j++)
                {
                    int dif = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1,
                                             m[i, j - 1] + 1),
                                             m[i - 1, j - 1] + dif);
                }

            return m[string1.Length, string2.Length];
        }
    }
}
