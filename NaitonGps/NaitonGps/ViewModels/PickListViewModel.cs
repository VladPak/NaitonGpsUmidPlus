using NaitonGps.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NaitonGps.ViewModels
{
    public class PickListViewModel : INotifyPropertyChanged
    {
        public ICommand RefreshCommand { protected set; get; }

        public List<PickList> Picklist { get; set; }


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


        public PickListViewModel(List<PickList> pickList)
        {
            Picklist = pickList;

            RefreshCommand = new Command<string>((key) =>
            {
                Picklist = pickList;
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
