using NaitonGps.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistSearchItemBottomPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PicklistSearchItemBottomPopup()
        {
            InitializeComponent();
            
            BindingContext = new RacksViewModel();
            
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