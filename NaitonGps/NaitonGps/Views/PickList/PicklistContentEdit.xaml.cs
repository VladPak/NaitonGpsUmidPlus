﻿using NaitonGps.Helpers;
using NaitonGps.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistContentEdit : ContentPage
    {
        private readonly int pListId;
        public string modeResult = Preferences.Get("userMode", string.Empty);

        public PicklistContentEdit(int pickListId)
        {
            pListId = pickListId;
            CloseAllPopup();
            InitializeComponent();
            var pickListItems = DataManager.GetPickListItems(pickListId);
            BindingContext = new PicklistContentDataViewModel(pickListItems);

            switch (modeResult)
            {
                case "readOnly":
                    gridToHide.IsVisible = false;
                    break;
                case "readAndEdit":
                    gridToHide.IsVisible = true;
                    break;
                default:
                    gridToHide.IsVisible = true;
                    DisplayAlert("", "Server error. Please contact app development team", "Ok");
                    break;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistPrintLabelsBottomPopup());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await DisplayAlert("", "Save and Print", "Ok");            
        }

        public async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistQuantityBottomPopup());
        }

        private async void ShowRackList(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistSearchItemBottomPopup((int)((TappedEventArgs)e).Parameter));
        }
    }
}