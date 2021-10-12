using NaitonGps.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NaitonGps.ViewModels
{
    public class PicklistContentDataViewModel : INotifyPropertyChanged
    {
        public ICommand RefreshCommand { protected set; get; }

        public List<PickListItem> PickListItems { get; set; }
        public int PickListId { get; set; }


        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }

            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged("IsRefreshing");

                }
            }

        }

        public PicklistContentDataViewModel(List<PickListItem> pickListItems)
        {
            if (pickListItems.Count > 0)
                PickListId = pickListItems.First().PickListId;

            PickListItems = pickListItems;

            RefreshCommand = new Command<string>((key) =>
            {
                PickListItems = pickListItems;
                IsRefreshing = false;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
