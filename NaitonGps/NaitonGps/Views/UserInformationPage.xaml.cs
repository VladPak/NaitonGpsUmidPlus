using NaitonGps.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
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
    public partial class UserInformationPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static double screenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool isSmallScreen { get; } = screenWidth <= 480;
        public static bool isBigScreen { get; } = screenWidth >= 480;

        public UserInformationPage()
        {
            InitializeComponent();
            UserLoginDetails userLoginDetails = JsonConvert.DeserializeObject<UserLoginDetails>((string)Application.Current.Properties["UserDetail"]);
            //UserDetails userDetails = JsonConvert.DeserializeObject<UserDetails>((string)Application.Current.Properties["UserLoginDetail"]);

            userInfoEmail.Text = "Email: " + userLoginDetails.userEmail;
            //userInfoName.Text = "Name" + userDetails.EmployeeName;
        }

        private async void Close(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
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
    }
}