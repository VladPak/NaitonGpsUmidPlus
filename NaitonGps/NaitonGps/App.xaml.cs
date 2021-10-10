using NaitonGps.Models;
using NaitonGps.Services;
using NaitonGps.Views;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps
{
    public partial class App : Application
    {
        public static double screenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool isSmallScreen { get; } = screenWidth <= 480;
        public static bool isBigScreen { get; } = screenWidth >= 480;

        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;

            if (isSmallScreen)
            {
                if (!isLoggedIn)
                {
                    var nav = new NavigationPage(new LoginScreenNaiton());
                    MainPage = nav;
                }
                else
                {
                    var nav = new NavigationPage(new ContinueSession());
                    MainPage = nav;

                    //try
                    //{
                    //    UserLoginDetails userData = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);

                    //    Session session = new Session(userData.userEmail,
                    //                                    userData.userPassword,
                    //                                    userData.isEncrypted,
                    //                                    userData.appId,
                    //                                    userData.appVersion,
                    //                                    userData.domain,
                    //                                    null);

                    //    await session.CreateByConnectionProviderAddressAsync(userData.connectionProviderAddress);
                    //    //var nav = new NavigationPage(new MainNavigationPage());
                    //    //MainPage = nav;
                    //    Application.Current.MainPage = new MainNavigationPage();

                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.ToString());
                    //}
                }
            }
            else if (isBigScreen)
            {
                if (!isLoggedIn)
                {
                    var nav = new NavigationPage(new LoginScreenNaitonBigScreen());
                    MainPage = nav;
                }
                else
                {
                    var nav = new NavigationPage(new ContinueSession());
                    MainPage = nav;
                }
            }
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
