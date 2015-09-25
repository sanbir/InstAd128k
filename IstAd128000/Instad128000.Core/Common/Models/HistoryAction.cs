using Instad128000.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Models
{
    public class HistoryAction
    {
        public string Link { get; set; }
        public string Comment { get; set; }
        public RequestType Type { get; set; }
    }
}
