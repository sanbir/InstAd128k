using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Instad128000.Core.Common.Enums;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Logger;
using Instad128000.Core.Extensions;
using Microsoft.Practices.Unity;

namespace Instad128000.Core.Helpers
{
    public class SentenceObfuscator
    {
        private IDataStringService DataStringService { get; set; }

        public SentenceObfuscator(string strToObfuscate, IDataStringService dataStringService)
        {
            this.DataStringService = dataStringService;

            PreviousObfuscated = new List<string>();
            InitialSentence = strToObfuscate;
            PreviousObfuscated.Add(strToObfuscate);

            Rand = new Random();
        }

        private string InitialSentence { get; set; }

        public string GetInitialString()
        {
            return InitialSentence;
        }

        public List<string> GetHistoryOfSentenceChanges()
        {
            return PreviousObfuscated;
        }

        private List<string> PreviousObfuscated { get; set; }

        private Random Rand { get; set; }

        private int LevenshteinTryCount { get; set; }

        public void ChangeSentence(string newSentence)
        {
            InitialSentence = newSentence; 
            Dispose();
        }

        public void Dispose()
        {
            LevenshteinTryCount = 0;
            PreviousObfuscated = new List<string>();
        }

        public string Next(int changesNumber = 0, string toObfuscate = null)
        {
            string currentString;
            if (toObfuscate == null)
            {
                currentString = PreviousObfuscated.Last();
            }
            else
            {
                currentString = toObfuscate;
            }
            var stringLength = currentString.Length;
            if (changesNumber == 0)
            {
                changesNumber = Rand.Next(0, stringLength / 8);
            }

            for (var i = 0; i < changesNumber; i++)
            {
                var possibleChanges = Enum.GetValues(typeof(SentenceChanges));
                var currentChange = Rand.Next(0, possibleChanges.Length - 1);

                switch ((SentenceChanges)currentChange)
                {
                    case SentenceChanges.AddChar:
                        currentString = AddChar(currentString);
                        break;
                    case SentenceChanges.CapitalizeChar:
                        currentString = CapitalizeChar(currentString);
                        break;
                    case SentenceChanges.CharToSymbol:
                        currentString = CharToSymbol(currentString);
                        break;
                    case SentenceChanges.RepeatChar:
                        currentString = RepeatChar(currentString);
                        break;
                    case SentenceChanges.DecapitalizeChar:
                        currentString = DecapitalizeChar(currentString);
                        break;
                }
            }

            var leven = LevenshteinDistance.Find(currentString, PreviousObfuscated.Last());
            if (leven < stringLength / 8 && LevenshteinTryCount<10)
            {
                LevenshteinTryCount ++;
                Next(stringLength / 8 - leven, currentString);
            }
            LevenshteinTryCount = 0;
            PreviousObfuscated.Add(currentString);
            return currentString;
        }

        private string AddChar(string toChange)
        {
            var addable = DataStringService.Where(x=>x.IsAddable).ToArray();
            return toChange + addable[Rand.Next(0, addable.Count() - 1)].String;
        }

        private string CapitalizeChar(string toChange)
        {
            var index = Rand.Next(0, toChange.Length - 1);
            toChange = toChange.ChangeCharOnIndexTo(index, Char.ToUpper(toChange[index]));
            return toChange;
        }
        
        private string CharToSymbol(string toChange)
        {
            var matchedChars = new Dictionary<int, char>();
            for (var i=0; i<toChange.Length;i++)
            {
                if (DataStringService.MatchString(toChange[i].ToString(), SentenceChanges.CharToSymbol))
                {
                    matchedChars.Add(i,toChange[i]);
                }
            }

            if (matchedChars.Count == 0) return toChange;

            var indexToChange = matchedChars.ToArray()[Rand.Next(0, matchedChars.Count - 1)].Key;
            var changers = DataStringService.GetSymbolsByString(toChange[indexToChange].ToString());

            toChange = toChange.ChangeCharOnIndexTo(indexToChange, changers[Rand.Next(0, changers.Count - 1)]);

            return toChange;
        }
        
        private string RepeatChar(string toChange)
        {
            var matchedChars = new Dictionary<int, char>();
            for (var i = 0; i < toChange.Length; i++)
            {
                if (DataStringService.MatchString(toChange[i].ToString(), SentenceChanges.RepeatChar))
                {
                    matchedChars.Add(i, toChange[i]);
                }
            }

            if (matchedChars.Count == 0) return toChange;

            var indexToChange = matchedChars.ToArray()[Rand.Next(0, matchedChars.Count - 1)].Key;
            var toChangeString = toChange[indexToChange].ToString();
            var changers = DataStringService.Where(x => x.String == toChangeString).ToList();
            var randomChanger = Rand.Next(0, changers.Count() - 1);
            var toInsertStr = changers[randomChanger].String + changers[randomChanger].String;
            toChange = toChange.ChangeCharOnIndexTo(indexToChange, toInsertStr);
            
            return toChange;
        }

        private string DecapitalizeChar(string toChange)
        {
            var index = Rand.Next(0, toChange.Length - 1);
            toChange = toChange.ChangeCharOnIndexTo(index, Char.ToLower(toChange[index]));
            return toChange;
        }
    }
}
