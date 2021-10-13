using NaitonGps.Models;
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
    public partial class PicklistQuantityBottomPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PickListItem PickListItem { get; set; }
        public PicklistQuantityBottomPopup(ref PickListItem item)
        {
            InitializeComponent();
            PickListItem = item;
            BindingContext = item;
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