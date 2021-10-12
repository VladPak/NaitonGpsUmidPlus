using NaitonGps.Models;
using NaitonGps.Services;
using NaitonGps.Views;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps
{
    public partial class Application : Xamarin.Forms.Application
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        public Application()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            base.OnStart();
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") && Convert.ToBoolean(Current.Properties["IsLoggedIn"]);

            if (IsSmallScreen)
            {
                if (!isLoggedIn)
                {
                    var nav = new NavigationPage(new LoginScreenNaiton());
                    MainPage = nav;
                }
                else
                {

                    var nav = new NavigationPage(new MainNavigationPage());
                    MainPage = nav;
                }
            }
            else if (IsBigScreen)
            {
                if (!isLoggedIn)
                {
                    var nav = new NavigationPage(new LoginScreenNaitonBigScreen());
                    MainPage = nav;
                }
                else
                {
                    var nav = new NavigationPage(new LoginScreenNaitonBigScreen());
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
