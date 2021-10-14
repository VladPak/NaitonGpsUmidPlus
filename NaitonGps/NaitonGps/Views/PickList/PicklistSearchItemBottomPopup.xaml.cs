using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.ViewModels;
using Rg.Plugins.Popup.Services;
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
    public partial class PicklistSearchItemBottomPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        public PicklistSearchItemBottomPopup(ref PickListItem item)
        {
            InitializeComponent();

            if (IsSmallScreen)
            {
                slChange.Margin = new Thickness(0);
                slChange.Padding = new Thickness(-5);
                imgScan.WidthRequest = 20;
                lblScanToHIde.IsVisible = false;
                imgCloseP.HeightRequest = 25;
                imgCloseP.WidthRequest = 25;
                lblRacSelect.FontSize = 22;
                entSearch.FontSize = 15;
                lblScanToHIde.IsVisible = false;
                columnToAlter.Width = new GridLength(1, GridUnitType.Star);
            }
            else if (IsBigScreen)
            {
                slChange.Margin = new Thickness(-5, 0, -5, 0);
                slChange.Padding = new Thickness(0);
                imgScan.WidthRequest = 20;
                lblScanToHIde.IsVisible = true;
                imgCloseP.HeightRequest = 30;
                imgCloseP.WidthRequest = 30;
                lblRacSelect.FontSize = 28;
                entSearch.FontSize = 20;
                lblScanToHIde.IsVisible = true;
                columnToAlter.Width = new GridLength(1.5, GridUnitType.Star);
            }

            var rackList = DataManager.GetPickRacks(item.DeliveryOrderDetailsId,item.Quantity);
            BindingContext = new RacksViewModel(rackList);
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("", "Scan is clicked", "Ok");
        }
    }
}