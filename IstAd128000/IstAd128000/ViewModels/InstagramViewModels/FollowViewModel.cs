using Instad128000.Core.Common.Interfaces.Services;
using InstaSharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.ViewModels.InstagramViewModels
{
    public class FollowViewModel : CommonViewModel, INotifyPropertyChanged
    {
        public FollowViewModel()
        {
            TypedUserName = "olesya_sha";
        }
        public string TypedUserName { get; set; }
        private List<User> _returnList;
        public List<User> UserList
        {
            get { return _returnList; }
            set
            {
                _returnList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
