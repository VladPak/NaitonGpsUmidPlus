using NaitonGps.Models;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNextLoop.Markups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContinueSession : ContentPage
    {
        public static double screenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool isSmallScreen { get; } = screenWidth <= 480;
        public static bool isBigScreen { get; } = screenWidth >= 480;

        public ContinueSession()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserLoginDetails userData = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);

                Session session = new Session(userData.userEmail,
                                                userData.userPassword,
                                                userData.isEncrypted,
                                                userData.appId,
                                                userData.appVersion,
                                                userData.domain,
                                                null);

                await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");
                Application.Current.MainPage = new NavigationPage(new MainNavigationPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;

            if (isSmallScreen)
            {
                Application.Current.MainPage = new NavigationPage(new LoginScreenNaiton());
            }
            else if (isBigScreen)
            {
                Application.Current.MainPage = new NavigationPage(new LoginScreenNaitonBigScreen());
            }

            await Navigation.PopToRootAsync();
        }
    }
}