using NaitonGps.Models;
using NaitonGps.Services;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
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
    public partial class NotificationsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static double screenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool isSmallScreen { get; } = screenWidth <= 480;
        public static bool isBigScreen { get; } = screenWidth >= 480;

        public NotificationsPage()
        {
            InitializeComponent();

      //UserDetails userLoginDetails = JsonConvert.DeserializeObject<UserDetails>((string)Application.Current.Properties["UserDetail"]);
      UserLoginDetails userLoginDetails = AuthenticationService.User;

      //userEmail.Text = "Name: " + userLoginDetails.EmployeeName;
    }

        private async void Logout(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();

            Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.FalseString;

            if (isSmallScreen)
            {
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new LoginScreenNaiton());
            }
            else if (isBigScreen)
            {
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new LoginScreenNaitonBigScreen());
            }

            await Navigation.PopToRootAsync();
        }

        private async void Close(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}