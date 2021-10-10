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
                ////var response = await ApiService.GetWebService(Preferences.Get("loginCompany", string.Empty));

                //string webservice = String.Format("https://connectionprovider.naiton.com/DataAccess/{0}/restservice/address", Preferences.Get("loginCompany", string.Empty));

                //var httpClient = new HttpClient();
                //var response = await httpClient.GetAsync(webservice);
                //var responseContent = await response.Content.ReadAsStringAsync();
                //var rsToString = responseContent.ToString();

                //Preferences.Set("webservicelink", rsToString);


                //if (response)
                //{
                UserLoginDetails userData = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);

                Session session = new Session(userData.userEmail,
                                                userData.userPassword,
                                                userData.isEncrypted,
                                                userData.appId,
                                                userData.appVersion,
                                                userData.domain,
                                                null);

                await session.CreateByConnectionProviderAddressAsync(userData.connectionProviderAddress);
                //MainPage = new MainNavigationPage();
                Application.Current.MainPage = new NavigationPage(new MainNavigationPage());

            }
            //    }
            //    else
            //    {
            //        await App.Current.MainPage.DisplayAlert("", "Ghtun", "Ok");
            //    }
            //}
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