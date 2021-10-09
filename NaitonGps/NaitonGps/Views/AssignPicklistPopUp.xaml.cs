using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class AssignPicklistPopUp : PopupPage
    {
        public string mode;
        public AssignPicklistPopUp()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var currentPage = new AssignPicklistPopUp();
            mode = "readOnly";
            Preferences.Set("userMode", mode);
            //await Navigation.RemovePopupPageAsync(currentPage);
            await Navigation.PushModalAsync(new PicklistContentEdit());

        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var currentPage = new AssignPicklistPopUp();
            mode = "readAndEdit";
            Preferences.Set("userMode", mode);
            //await Navigation.RemovePopupPageAsync(currentPage);
            await Navigation.PushModalAsync(new PicklistContentEdit());

        }

    }
}