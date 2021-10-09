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
    public partial class PicklistContent : ContentPage
    {
        public static double screenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool isSmallScreen { get; } = screenWidth <= 480;
        public static bool isBigScreen { get; } = screenWidth >= 480;

        public PicklistContent()
        {
            InitializeComponent();
            BindingContext = new PicklistContentDataViewModel(new List<PicklistContentDataViewModel>());

            //if (isSmallScreen)
            //{
            //    gridMain.Margin = new Thickness(10, 15, 10, 20);
            //    rv.Margin = new Thickness(0,0,0,0);
                
            //    iconBack.WidthRequest = 25;
            //    iconBack.HeightRequest = 25;
            //    iconListChange.HeightRequest = 25;
            //    iconListChange.WidthRequest = 25;

            //    lblHeaderTitle.FontSize = 18;
            //    lblButtonStartPicking.FontSize = 16; 

            //}
            //else if (isBigScreen)
            //{
            //    gridMain.Margin = new Thickness(10, 0, 10, 0);
            //    rv.Margin = new Thickness(0, -10, 0, 0);
                
            //    iconBack.WidthRequest = 30;
            //    iconBack.HeightRequest = 30;
            //    iconListChange.HeightRequest = 30;
            //    iconListChange.WidthRequest = 30;

            //    lblHeaderTitle.FontSize = 22;
            //    lblButtonStartPicking.FontSize = 20;

            //}

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await DisplayAlert("", "The filter btn clicked", "Ok");
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            //DisplayAlert("", "Call popUp", "Ok");
            await PopupNavigation.Instance.PushAsync(new AssignPicklistPopUp());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            DisplayAlert("", "The item is clicked", "Ok");
        }
    }
}