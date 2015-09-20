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
        public RequestResult(string commentText, long victimsId, long userId, string link, RequestType type, string postId)
        {
            Id = Guid.NewGuid();
            CommentText = commentText;
            VictimsId = victimsId;
            UserId = userId;
            Link = link;
            Type = type;
            PostId = postId;

        }
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public long VictimsId { get; set; }
        public long UserId { get; set; }
        public string Link { get; set; }
        public string PostId { get; set; }
        public RequestType Type { get; set; }
    }
}
