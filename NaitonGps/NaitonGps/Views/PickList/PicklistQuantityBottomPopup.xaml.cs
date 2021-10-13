using NaitonGps.Models;
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
    public partial class PicklistQuantityBottomPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 360;
        public static bool IsBigScreen { get; } = ScreenWidth >= 360;

        public PickListItem PickListItem { get; set; }
        public PicklistQuantityBottomPopup(ref PickListItem item)
        {
            InitializeComponent();
            PickListItem = item;
            BindingContext = item;

            if (IsSmallScreen)
            {
                gridMain.Margin = new Thickness(0, 0, 0, 0);
                entQuantity.Margin = new Thickness(0, 0, 0, 0);
                imgCloseP.HeightRequest = 23;
                imgCloseP.WidthRequest = 23;

                lblQunaitty.FontSize = 20;
                lblQunaitty.VerticalOptions = LayoutOptions.Center;
                frameToChange.HeightRequest = 20;
                lblApply.FontSize = 15;

            }
            else if (IsBigScreen)
            {
                gridMain.Margin = new Thickness(0, 0, 0, 15);
                entQuantity.Margin = new Thickness(0, -20, 0, 0);
                imgCloseP.HeightRequest = 30;
                imgCloseP.WidthRequest = 30;

                lblQunaitty.FontSize = 28;
                lblQunaitty.VerticalOptions = LayoutOptions.Start;
                frameToChange.HeightRequest = 30;
                lblApply.FontSize = 18;

            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await DisplayAlert("", "Apply btn is clicked", "Ok");
        }

    }
}