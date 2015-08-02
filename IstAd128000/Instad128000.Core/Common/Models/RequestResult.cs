using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Enums;
using Instad128000.Core.Common.Interfaces;

namespace Instad128000.Core.Common.Models
{
    public class RequestResult : IRequestResult
    {
        public RequestResult(string commentText, int victimsId, int userId, RequestType type)
        {
            Id = Guid.NewGuid();
            CommentText = commentText;
            VictimsId = victimsId;
            UserId = userId;
            Type = type;
        }
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public int VictimsId { get; set; }
        public int UserId { get; set; }
        public RequestType Type { get; set; }
    }
}
