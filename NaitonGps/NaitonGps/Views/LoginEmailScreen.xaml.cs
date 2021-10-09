using NaitonGps.Services;
using Plugin.Connectivity;
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
    public partial class LoginEmailScreen : ContentPage
    {
        public LoginEmailScreen()
        {
            InitializeComponent();
            //lblCompanyName.Text = $"Web Service: {Preferences.Get("webservicelink", string.Empty)}";

            //entEmail.ReturnCommand = new Command(() => entEmail.Focus());
            
        }

        private void TapBackToCompanySelect_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            entEmail.Text = string.Empty;
        }

        private async void TapEmailSelect_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("loginEmail", entEmail.Text);
            var userEmail = entEmail.Text;
            var emailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var providerPattern = @"\b(naitongps)\b";
            var webServiceName = Preferences.Get("loginCompany", string.Empty);

            if (CrossConnectivity.Current.IsConnected)
            {                    
                if (string.IsNullOrEmpty(userEmail))
                {
                    await DisplayAlert("", "Invalid email", "Ok");
                }
                else
                {
                    if (Regex.IsMatch(userEmail, emailPattern))
                    {
                        if (Regex.IsMatch(webServiceName, providerPattern))
                        {
                            //await Task.Delay(150);
                            await Navigation.PushModalAsync(new LoginPasswordScreen());
                            entEmail.Text = string.Empty;
                            //Application.Current.MainPage = new MainNavigationPage();
                        }
                        else
                        {
                            await DisplayAlert("", "Only naitongps users allowed", "Ok");
                        }
                    }
                    else if (!Regex.IsMatch(userEmail, emailPattern))
                    {
                        await DisplayAlert("", "Wrong email format", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("", "Email input error.", "Ok");
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