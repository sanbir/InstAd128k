using Instad128000.Core.Common.Models;
using Instad128000.Core.Common.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Extensions
{
    public static class RequestResultExtensions
    {
        public static DataRequestResult ToDataRequestResult(this RequestResult res)
        {
            var output = new DataRequestResult();

            output.CommentText = res.CommentText;
            output.Link = res.Link;
            output.UserId = res.UserId;
            output.VictimsId = res.VictimsId;
            output.PostId = res.PostId;
            output.Type = res.Type;

            return output;
        }
    }
}
