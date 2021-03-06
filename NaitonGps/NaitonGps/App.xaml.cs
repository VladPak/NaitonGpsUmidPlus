using NaitonGps.Models;
using NaitonGps.Services;
using NaitonGps.Views;
using Newtonsoft.Json;
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
            var navigationButtonSizes = new Style(typeof(Image))
            {
                Setters = {
                    new Setter { Property = Image.HeightRequestProperty,    Value = 30 },
                    new Setter { Property = Image.WidthRequestProperty,    Value = 30 }
                }
            };
            
            Resources = new ResourceDictionary();
            Resources.Add("navigationButtonSizes", navigationButtonSizes);

            InitializeComponent();
        }

        protected override void OnStart()
        {
            base.OnStart();
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") && Convert.ToBoolean(Current.Properties["IsLoggedIn"]);

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (IsSmallScreen)
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
                else if (IsBigScreen)
                {
                    if (!isLoggedIn)
                    {
                        var nav = new NavigationPage(new LoginScreenNaitonAndroidBig());
                        MainPage = nav;
                    }
                    else
                    {
                        var nav = new NavigationPage(new ContinueSession());
                        MainPage = nav;
                    }
                }
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                if (IsSmallScreen)
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
                else if (IsBigScreen)
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

        }

    }
}
