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
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        private readonly int pListId;
        public string mode;
        public AssignPicklistPopUp(int pickListId)
        {
            InitializeComponent();

            if (IsSmallScreen)
            {
                slChange.Spacing = 10;
                closePop.HeightRequest = 25;
                closePop.WidthRequest = 25;

                frameWW.HeightRequest = 19;
                frameChange.HeightRequest = 19;
                lblAssign.FontSize = 18;
                lblPress.FontSize = 13;
                lblAssign.FontSize = 18;
                LblNooo.FontSize = 15;
                lblAssignN.FontSize = 15;
            }
            else if (IsBigScreen)
            {
                closePop.HeightRequest = 30;
                closePop.WidthRequest = 30;
                slChange.Spacing = 20;

                frameWW.HeightRequest = 30;
                frameChange.HeightRequest = 30;
                lblAssign.FontSize = 25;
                lblPress.FontSize = 16;
                lblAssign.FontSize = 25;
                LblNooo.FontSize = 18;
                lblAssignN.FontSize = 20;

            }

            pListId = pickListId;
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _ = new AssignPicklistPopUp(pListId);
            mode = "readOnly";
            Preferences.Set("userMode", mode);
            //await Navigation.RemovePopupPageAsync(currentPage);
            await Navigation.PushModalAsync(new PicklistContentEdit(pListId));

        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            _ = new AssignPicklistPopUp(pListId);
            mode = "readAndEdit";
            Preferences.Set("userMode", mode);
            //await Navigation.RemovePopupPageAsync(currentPage);
            await Navigation.PushModalAsync(new PicklistContentEdit(pListId));

        }

    }
}