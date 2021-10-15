using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.ViewModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistItems : ContentPage
    {
        private readonly int pListId;
        public string mode;

        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;
                
        public PicklistItems(int pickListId)
        {
            InitializeComponent();
            if (IsSmallScreen)
            {
                gridMain.Margin = new Thickness(10, 0, 10, 13);
                rowToChange.Height = new GridLength(1.3, GridUnitType.Star);
                iconBack.HeightRequest = 25;
                iconBack.WidthRequest = 25;

                iconListChange.HeightRequest = 25;
                iconListChange.WidthRequest = 25;
                lblHeaderTitle.FontSize = 18;
                rv.Margin = new Thickness(0, 0, 0, 0);
                
            }
            else if (IsBigScreen)
            {
                rv.Margin = new Thickness(0, -10, 0, 0);

                gridMain.Margin = new Thickness(10, 15, 10, 20);
                rowToChange.Height = new GridLength(1, GridUnitType.Star);
                iconBack.HeightRequest = 30;
                iconBack.WidthRequest = 30;

                iconListChange.HeightRequest = 30;
                iconListChange.WidthRequest = 30;
                lblHeaderTitle.FontSize = 22;

            }

            pListId = pickListId;
            var pickListItems = DataManager.GetPickListItems(pickListId);
            BindingContext = new PicklistContentDataViewModel(pickListItems);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await DisplayAlert("", "The filter btn clicked", "Ok");
        }

        private async void StartPicking(object sender, EventArgs e)
        {            
            //await PopupNavigation.Instance.PushAsync(new AssignPicklistPopUp(pListId));

            //_ = new AssignPicklistPopUp(pListId);
            mode = "readAndEdit";
            Preferences.Set("userMode", mode);
            //await Navigation.RemovePopupPageAsync(currentPage);
            await Navigation.PushModalAsync(new PicklistContentEdit(pListId));
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            DisplayAlert("", "The item is clicked", "Ok");
        }
    }
}