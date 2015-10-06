using Instad128000.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Extensions;

namespace Instad128000.Core.Common.Exceptions
{
    //todo: допилить этот класс
    public class InstAdException : Exception
    {
        public InstAdErrors Error { get; }
        public int Code { get; }
        public override string Message { get; }

        public InstAdException(InstAdErrors error) : base("Code: " + (int)error + "| Message: " + error.GetEnumDescription())
        {
            Error = error;
            Code = (int)Error;
            Message = Error.GetEnumDescription(); 
        }
    }
}
