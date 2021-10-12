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
        public static double screenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool isSmallScreen { get; } = screenWidth <= 480;
        public static bool isBigScreen { get; } = screenWidth >= 480;

        public Application()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            base.OnStart();
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
