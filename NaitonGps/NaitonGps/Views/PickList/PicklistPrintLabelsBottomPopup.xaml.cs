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
    public partial class PicklistPrintLabelsBottomPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public int initialValue = 0;
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 360;
        public static bool IsBigScreen { get; } = ScreenWidth >= 360;

        public PicklistPrintLabelsBottomPopup()
        {
            InitializeComponent();
            entQuantity.Text = initialValue.ToString();

            if (IsSmallScreen)
            {
                mainGrid.Margin = new Thickness(0, 0, 0, 0);
                mainGrid.RowSpacing = 8;


                rowChange2.Height = new GridLength(0.4, GridUnitType.Star);
                rowChange5.Height = new GridLength(0.4, GridUnitType.Star);
                imgCloseP.HeightRequest = 23;
                imgCloseP.WidthRequest = 23;


                lblPrintLabels.FontSize = 20;
                lblChangezz.FontSize = 14;
                lblChangezz.Margin = new Thickness(0, -35, 0, 0);
                lblNumberzz.FontSize = 14;
                entQuantity.FontSize = 17;
                frameToChange.HeightRequest = 20;
                lblSave.FontSize = 15;
            }
            else if (IsBigScreen)
            {
                mainGrid.Margin = new Thickness(0, 0, 0, 15);
                mainGrid.RowSpacing = 10;


                rowChange2.Height = new GridLength(0.6, GridUnitType.Star);
                rowChange5.Height = new GridLength(0.5, GridUnitType.Star);
                imgCloseP.HeightRequest = 30;
                imgCloseP.WidthRequest = 30;


                lblPrintLabels.FontSize = 28;
                lblChangezz.FontSize = 17;
                lblChangezz.Margin = new Thickness(0, -60, 0, 0);
                lblNumberzz.FontSize = 16;
                entQuantity.FontSize = 20;
                frameToChange.HeightRequest = 30;
                lblSave.FontSize = 18;

            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await DisplayAlert("", "Save btn is clicked", "Ok");
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            int newValue = initialValue -= 1;
            if (newValue < 0)
            {
                DisplayAlert("", "Negative value is not accepted", "Ok");
                initialValue = 0;
                entQuantity.Text = initialValue.ToString();
            }
            else
            {
                entQuantity.Text = newValue.ToString();
            }
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            int newValue2 = initialValue += 1;
            if (newValue2 > 1000000000)
            {
                DisplayAlert("", "Max value is 1 billion units", "Ok");
            }
            else
            {
                entQuantity.Text = newValue2.ToString();
            }
        }
    }
}