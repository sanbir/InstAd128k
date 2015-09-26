using Instad128000.Core.Common.Models;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Interfaces
{
    public interface IInstaUser : ISocialNetworkUser
    {
        Task<List<User>> GetContactsListAsync(string userName);
        Task<List<User>> AddToContactsAllContactsOfUserAsync(string userName);
        Task<List<RequestResult>> LikeByTagAsync(List<string> tag, TimeSpan workPeriod);
        Task<List<RequestResult>> CommentByTagAsync(List<string> tag, string commentText, TimeSpan workPeriod);
        Task<TagsResponse> SearchForTagsAsync(string tagPart);
    }
}
