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

        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;
                
        public PicklistItems(int pickListId)
        {
            InitializeComponent();
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
            await PopupNavigation.Instance.PushAsync(new AssignPicklistPopUp(pListId));
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            DisplayAlert("", "The item is clicked", "Ok");
        }
    }
}