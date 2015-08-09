using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Instad128000.Core.Common.Enums;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Extensions;

namespace Instad128000.Core.Helpers
{
    public class SentenceObfuscator
    {
        private ICharToSymbolService CharSymbolService { get; set; }
        private IRepeatableCharsService RepeatableCharsService { get; set; }

        public SentenceObfuscator(string strToObfuscate, ICharToSymbolService charSymbolService, IRepeatableCharsService repeatableCharsService)
        {
            this.CharSymbolService = charSymbolService;
            this.RepeatableCharsService = repeatableCharsService;
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

        private List<string> PreviousObfuscated { get; set; }

        private Random Rand { get; set; }

        public void ChangeSentence(string newSentence)
        {
            InitialSentence = newSentence; 
            Dispose();
        }

        public void Dispose()
        {
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
            if (leven < stringLength / 8)
            {
                Next(stringLength / 8 - leven, currentString);
            }
            PreviousObfuscated.Add(currentString);
            return currentString;
        }

        private string AddChar(string toChange)
        {
            return toChange;
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
                if (CharSymbolService.MatchChar(toChange[i]))
                {
                    matchedChars.Add(i,toChange[i]);
                }
            }

            if (matchedChars.Count == 0) return toChange;

            var indexToChange = matchedChars.ToArray()[Rand.Next(0, matchedChars.Count - 1)].Key;
            var changers = CharSymbolService.GetSymbolsByChar(toChange[indexToChange]);

            toChange = toChange.ChangeCharOnIndexTo(indexToChange, changers[Rand.Next(0, changers.Count - 1)]);

            return toChange;
        }

        private string RepeatChar(string toChange)
        {
            var matchedChars = new Dictionary<int, char>();
            for (var i = 0; i < toChange.Length; i++)
            {
                if (RepeatableCharsService.MatchChar(toChange[i]))
                {
                    matchedChars.Add(i, toChange[i]);
                }
            }

            if (matchedChars.Count == 0) return toChange;

            var indexToChange = matchedChars.ToArray()[Rand.Next(0, matchedChars.Count - 1)].Key;
            var toChangeString = toChange[indexToChange].ToString();
            var changers = RepeatableCharsService.Where(x => x.Char == toChangeString).ToList();
            var randomChanger = Rand.Next(0, changers.Count() - 1);
            var toInsertStr = changers[randomChanger].Char + changers[randomChanger].Char;
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
