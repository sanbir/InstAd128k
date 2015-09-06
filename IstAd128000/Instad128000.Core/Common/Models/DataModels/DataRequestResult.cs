using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Enums;

namespace Instad128000.Core.Common.Models.DataModels
{
    public class DataRequestResult : BaseEntity
    {
        public string CommentText { get; set; }
        public int VictimsId { get; set; }
        public int UserId { get; set; }
        public string Link { get; set; }
        public RequestType Type { get; set; }
    }
}
