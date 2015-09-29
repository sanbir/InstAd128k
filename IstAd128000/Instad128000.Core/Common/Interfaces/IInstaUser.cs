using FourSquare.SharpSquare.Entities;
using Instad128000.Core.Common.Models;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Interfaces
{
    public interface IInstaUser : ISocialNetworkUser
    {
        Task<IEnumerable<InstaSharp.Models.User>> GetContactsListAsync(string userName);
        Task<IEnumerable<InstaSharp.Models.User>> AddToContactsAllContactsOfUserAsync(string userName);
        Task<IEnumerable<RequestResult>> LikeByTagAsync(TimeSpan workPeriod);
        Task<IEnumerable<RequestResult>> CommentByTagAsync(string commentText, TimeSpan workPeriod);
        Task<TagsResponse> SearchForTagsAsync(string tagPart);

        ObservableCollection<TagsCount> TagsToProcess { get; set; }
        ObservableCollection<Venue> LocationsToProcess { get; set; }
    }
}
