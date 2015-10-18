using Instad128000.Core.Helpers.SocialNetworksUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InstAd128000.ViewModels.InstagramViewModels
{
    public class InstagramTabViewModel : INotifyPropertyChanged
    {
        public InstagramTabViewModel()
        {
            this.CancelToken = new CancellationTokenSource();
            this.CancelWorkAction = () =>
            {
                CancelToken.Cancel();
            };
            CancelToken.Token.Register(() => 
            {
                this.CancelToken = new CancellationTokenSource();
                NotifyPropertyChanged(nameof(CancelToken));                                 
            });
        }

        public Action CancelWorkAction { get; private set; }
        public CancellationTokenSource CancelToken { get; private set; }

        public void SetInstaUserModelChangedEventHandler()
        {
            UserFactory.Insta.PropertyChanged += Insta_PropertyChanged;
        }

        private void Insta_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsLogged = UserFactory.Insta.IsLogged;
        }

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
