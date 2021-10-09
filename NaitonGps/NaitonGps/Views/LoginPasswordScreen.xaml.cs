using NaitonGps.Services;
using Plugin.Connectivity;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPasswordScreen : ContentPage
    {
        public LoginPasswordScreen()
        {
            InitializeComponent();
        }

        private async void tapBackToEmailScreen_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            entPassword.Text = string.Empty;
        }


        private async void tapLogin_Tapped(object sender, EventArgs e)
        {
            var userEmail = Preferences.Get("loginEmail", string.Empty);
            var emailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var providerPattern = @"\b(naitongps)\b";
            var webServiceName = Preferences.Get("loginCompany", string.Empty);
            var userPassword = entPassword.Text;

            if (CrossConnectivity.Current.IsConnected)
            {
                if (string.IsNullOrEmpty(userPassword) || userPassword != "Gromit12")
                {
                    await DisplayAlert("", "Invalid password", "Ok");
                }
                else
                {
                    if (Regex.IsMatch(userEmail, emailPattern))
                    {
                        if (Regex.IsMatch(webServiceName, providerPattern))
                        {
                            //var checkAccess = Preferences.Get("loginCompany", string.Empty);
                            //var response = await ApiService.Login(userEmail, entPassword.Text);

                            var domain = Preferences.Get("loginCompany", string.Empty);
                            var webserviceLink = Preferences.Get("webservicelink", string.Empty);
                            string currentAppVersion = VersionTracking.CurrentVersion;

                            Session session = new Session(userEmail,
                                                          entPassword.Text,
                                                          false,
                                                          4,
                                                          currentAppVersion,
                                                          "naitongps",
                                                          null);
                            await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");

                            //if ()
                            //{
                            //Application.Current.MainPage = new MainPage();
                            Preferences.Set("token", SessionContext.Token);
                            Application.Current.MainPage = new MainNavigationPage();
                            //}
                            //else
                            //{
                            //    await DisplayAlert("", "You have problems with Web Service. Please contact the support center", "Ok");
                            //}
                        }
                        else
                        {
                            await DisplayAlert("", "Only naitongps users allowed", "Ok");
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(userPassword))
                    {
                        await DisplayAlert("", "Password field is empty", "Ok");
                    }
                    else if (!Regex.IsMatch(userEmail, emailPattern))
                    {
                        await DisplayAlert("", "Wrong email format", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("", "Email or password input error.", "Ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("", "Check the Internet connection.", "Ok");
            }
        }
    }
}