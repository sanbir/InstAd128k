using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Enums;

namespace Instad128000.Core.Common.Interfaces
{
    interface IRequestResult
    {
        Guid Id { get; set; }
        string CommentText { get; set; }
        long VictimsId { get; set; }
        long UserId { get; set; }
        RequestType Type { get; set; }
    }
}
