using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.SentenceBoilTest
{
    public class Sentence
    {
        public Sentence(string initialString)
        {
            Initial = initialString;
            PreviousBolied = new List<string>();
        }

        private string Initial { get; set; }

        private List<string> PreviousBolied { get; set; }

        public void Flush()
        {
            PreviousBolied.RemoveRange(0, PreviousBolied.Count);
        }

    }
}
