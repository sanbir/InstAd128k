using FourSquare.SharpSquare.Entities;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models;
using Instad128000.Core.Extensions;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Interfaces
{
    public abstract class BaseInstaUser
    {
        public BaseInstaUser(string userName, string userPassword, string clientKey, string clientId, 
            IRequestService requestService, IDataStringService dataStringService)
        {
            RequestService = requestService;
            DataStringService = dataStringService;
            _tagsToProcess = new ObservableCollection<TagsCount>();
            _locationsToProcess = new ObservableCollection<Venue>();
            _currentActionResultsList = new ObservableCollection<RequestResult>();
            ClientId = clientId;
            ClientKey = clientKey;
            UserName = userName;
            UserPassword = userPassword;
            _tagsToProcess.CollectionChanged += _tagsToProcess_CollectionChanged;
            _locationsToProcess.CollectionChanged += _locationsToProcess_CollectionChanged;
            _currentActionResultsList.CollectionChanged += _currentActionResultsList_CollectionChanged;
        }

        public delegate void TagsChangedEventHandler();
        public event TagsChangedEventHandler TagsChanged;

        public delegate void LocationsChangedEventHandler();
        public event LocationsChangedEventHandler LocationsChanged;

        public delegate void ResultsChangedEventHandler();
        public event ResultsChangedEventHandler ResultsChanged;

        public abstract Task<IEnumerable<InstaSharp.Models.User>> GetContactsListAsync(string userName);
        public abstract Task<IEnumerable<InstaSharp.Models.User>> AddToContactsAllContactsOfUserAsync(string userName);
        public abstract Task<IEnumerable<RequestResult>> LikeByTagAsync(TimeSpan workPeriod);
        public abstract Task<IEnumerable<RequestResult>> CommentByTagAsync(string commentText, TimeSpan workPeriod);
        public abstract Task<TagsResponse> SearchForTagsAsync(string tagPart);

        protected IRequestService RequestService { get; set; }
        protected IDataStringService DataStringService { get; set; }

        protected void SaveToDb()
        {
            if (RequestService != null)
            {
                foreach (var ress in _currentActionResultsList)
                {
                    RequestService.Update(ress.ToDataRequestResult());
                }
                RequestService.Save();
            }
        }

        public abstract bool Authorize();

        public IEnumerable<TagsCount> TagsToProcess
        {
            get
            {
                return _tagsToProcess;
            }
            set
            {
                var toDelete = _tagsToProcess.Where(x => !value.Any(y => y == x));
                var toAdd = value.Where(x => !_tagsToProcess.Any(y => y == x));

                foreach (var del in toDelete)
                {
                    _tagsToProcess.Remove(del);
                }

                foreach (var add in toAdd)
                {
                    _tagsToProcess.Add(add);
                }
            }
        }
        public IEnumerable<Venue> LocationsToProcess
        {
            get
            {
                return _locationsToProcess;
            }
            set
            {
                var toDelete = _locationsToProcess.Where(x => !value.Any(y => y == x));
                var toAdd = value.Where(x => !_locationsToProcess.Any(y => y == x));

                foreach (var del in toDelete)
                {
                    _locationsToProcess.Remove(del);
                }

                foreach (var add in toAdd)
                {
                    _locationsToProcess.Add(add);
                }
            }
        }
        public IEnumerable<RequestResult> CurrentActionResults
        {
            get
            {
                return _currentActionResultsList;
            }
            set
            {
                var toDelete = _currentActionResultsList.Where(x => !value.Any(y => y == x));
                var toAdd = value.Where(x => !_currentActionResultsList.Any(y => y == x));

                foreach (var del in toDelete)
                {
                    _currentActionResultsList.Remove(del);
                }

                foreach (var add in toAdd)
                {
                    _currentActionResultsList.Add(add);
                }
            }
        }

        protected ObservableCollection<TagsCount> _tagsToProcess { get; set; }
        protected ObservableCollection<Venue> _locationsToProcess { get; set; }
        protected ObservableCollection<RequestResult> _currentActionResultsList { get; set; }

        private void _tagsToProcess_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (TagsChanged != null)
            {
                TagsChanged();
            }
        }
        private void _locationsToProcess_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (LocationsChanged != null)
            {
                LocationsChanged();
            }
        }
        private void _currentActionResultsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ResultsChanged != null)
            {
                ResultsChanged();
            }
        }

        protected string UserName { get; set; }
        protected string UserPassword { get; set; }

        protected string ClientKey { get; set; }
        protected string ClientId { get; set; }

        public long UserId { get; set; }
    }
}
