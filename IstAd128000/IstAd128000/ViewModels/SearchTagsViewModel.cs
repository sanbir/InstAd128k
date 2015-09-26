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
    public class SearchTagsViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public SearchTagsViewModel()
        {
            Result = new List<TagsCount>();
            Chosen = new ObservableCollection<string>();
            Chosen.CollectionChanged += Chosen_CollectionChanged;
        }
        
        private void Chosen_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Result");
        }

        private List<TagsCount> result { get; set; }
        private ObservableCollection<string> chosen { get; set; }

        public List<TagsCount> Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        public ObservableCollection<string> Chosen
        {
            get
            {
                return chosen ?? new ObservableCollection<string>();
            }
            set
            {
                chosen = value;
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
        public event NotifyCollectionChangedEventHandler CollectionChanged;

    }
}
