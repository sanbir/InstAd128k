using FourSquare.SharpSquare.Entities;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.ViewModels
{
    public class CommentByTagViewModel : CommonViewModel, INotifyPropertyChanged
    {
        public CommentByTagViewModel()
        {
            Tags = new List<TagsCount>();
        }
        
        private IEnumerable<TagsCount> _tags { get; set; }
        public IEnumerable<TagsCount> Tags
        {
            get
            {
                return _tags ?? (_tags = new List<TagsCount>());
            }
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        private IEnumerable<Venue> _locations { get; set; }
        public IEnumerable<Venue> Locations
        {
            get
            {
                return _locations ?? (_locations = new List<Venue>());
            }
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
