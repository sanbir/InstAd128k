using Instad128000.Core.Common.Models;
using InstaSharp.Models.Responses;
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
    public class SearchTagsViewModel : CommonViewModel, INotifyPropertyChanged
    {
        public SearchTagsViewModel()
        {
            Result = new List<TagsCount>();
            Chosen = new ObservableCollection<TagsCount>();
        }

        private IEnumerable<TagsCount> result { get; set; }
        private IEnumerable<TagsCount> chosen { get; set; }

        public IEnumerable<TagsCount> Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public IEnumerable<TagsCount> Chosen
        {
            get
            {
                return chosen ?? (chosen = new ObservableCollection<TagsCount>());
            }
            set
            {
                chosen = value;
                OnPropertyChanged(nameof(Chosen));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
