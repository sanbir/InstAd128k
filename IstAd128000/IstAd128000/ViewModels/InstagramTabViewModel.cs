using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.ViewModels
{
    public class InstagramTabViewModel : INotifyPropertyChanged
    {
        private bool _loggedIn;
        private bool _noProcessPerformed;
        public bool IsLogged
        {
            get { return _loggedIn; }
            set
            {
                _loggedIn = value;
                NotifyPropertyChanged(nameof(IsLogged));
                NotifyPropertyChanged(nameof(IsUiFreeForUser));
            }
        }
        public bool IsNoProcessPerformed
        {
            get { return _noProcessPerformed; }
            set
            {
                _noProcessPerformed = value;
                NotifyPropertyChanged(nameof(IsNoProcessPerformed));
                NotifyPropertyChanged(nameof(IsSomeProcessPerformed));
                NotifyPropertyChanged(nameof(IsUiFreeForUser));
            }
        }
        public bool IsUiFreeForUser
        {
            get { return IsLogged && IsNoProcessPerformed; }
        }

        public bool IsSomeProcessPerformed
        {
            get { return !IsNoProcessPerformed; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
