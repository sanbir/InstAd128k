using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Models.DataModels
{
    public class DataString : BaseEntity
    {
        public string String { get; set; }
        public string StringToSymbolSymbol { get; set; }
        public bool IsRepeatable { get; set; }
        public bool IsAddable { get; set; }
    }
}
