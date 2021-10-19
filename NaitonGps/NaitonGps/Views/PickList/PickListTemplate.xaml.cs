using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.ViewModels;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickListTemplate : Grid
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        public PickListTemplate()
        {
            InitializeComponent();

            if (IsSmallScreen)
            {
                gridWithScannerIcon.Margin = new Thickness(-5, -10, -5, -10);
                mainGridd.Margin = new Thickness(5,10,5,15);
                rowToAdjust.Height = new GridLength(0.6, GridUnitType.Star);
            }
            else if (IsBigScreen)
            {
                gridWithScannerIcon.Margin = new Thickness(-5, -14, -5, -10);
                mainGridd.Margin = new Thickness(10, 0, 10, 10);
                rowToAdjust.Height = new GridLength(0.4, GridUnitType.Star);
            }

            var pickList = DataManager.GetPickLists();
            BindingContext = new PickListViewModel(pickList);
        }

        public async void Move()
        {
            await ContentContainer.TranslateTo(0, -300, 30, Easing.Linear);
            await Header.TranslateTo(0, -300, 30, Easing.Linear);
            Header.IsVisible = true;
            ContentContainer.IsVisible = true;
            await Header.TranslateTo(0, 0, 500);
            await ContentContainer.TranslateTo(0, 0, 300);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
        }

        private async void PicklistContentPage(object sender, EventArgs e)
        {            
            await Navigation.PushModalAsync(new PicklistItems((int)((TappedEventArgs)e).Parameter));
        }

    }
}