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
        public string modeResult = Preferences.Get("userMode", string.Empty);

        public PicklistContentEdit()
        {
            CloseAllPopup();
            InitializeComponent();
            BindingContext = new Models.PickList();

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
            await DisplayAlert("", "Save And Print", "Ok");
            //await PopupNavigation.Instance.PushAsync(new PicklistQuantityBottomPopup());
        }

        public async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistQuantityBottomPopup());
        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistSearchItemBottomPopup());
        }
    }
}