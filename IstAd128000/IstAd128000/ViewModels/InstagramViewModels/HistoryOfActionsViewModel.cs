using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Enums;

namespace InstAd128000.ViewModels.InstagramViewModels
{
    public class HistoryOfActionsViewModel : CommonViewModel, INotifyPropertyChanged
    {
        public HistoryOfActionsViewModel()
        {
            _history = new List<HistoryAction>();
        }

        private IEnumerable<HistoryAction> _history { get; set; }
        public IEnumerable<HistoryAction> History {
            get
            {
                return _history;
            }
            set
            {
                _history = value;
                OnPropertyChanged(nameof(History));
                OnPropertyChanged(nameof(FullCountLike));
                OnPropertyChanged(nameof(FullCountComment));
                OnPropertyChanged(nameof(FullCountFollow));
            }
        }
        public int FullCountLike
        {
            get
            {
                if (History?.Count() == 0)
                {
                    return 0;
                }
                else
                {
                    return History.Where(x => x.Type == RequestType.Like).Count();
                }
            }
        }
        public int FullCountComment
        {
            get
            {
                if (History?.Count() == 0)
                {
                    return 0;
                }
                else
                {
                    return History.Where(x => x.Type == RequestType.Comment).Count();
                }
            }
        }
        public int FullCountFollow
        {
            get
            {
                if (History?.Count() == 0)
                {
                    return 0;
                }
                else
                {
                    return History.Where(x => x.Type == RequestType.Follow).Count();
                }
            }
        }

        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
