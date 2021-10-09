using NaitonGps.Services;
using Plugin.Connectivity;
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
    public partial class LoginCompanySelectScreen : ContentPage
    {
        int taps = 0;
        public LoginCompanySelectScreen()
        {
            InitializeComponent();
        }

        private async void TabCompanySelect_Tapped(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Preferences.Set("loginCompany", entCompany.Text);
                //Call Web service
                taps++;
                var response = await ApiService.GetWebService(entCompany.Text);

                if (response)
                {
                    if (taps == 1)
                    {
                        await Navigation.PushModalAsync(new LoginEmailScreen(), true);
                        entCompany.Text = string.Empty;
                        taps = 0;
                    }
                    else if (taps >= 2)
                    {
                        taps = 0;
                        await DisplayAlert("", "Please wait. Your request is being proceeded", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("", "Company does not exist. Please enter valid company name", "Ok");
                    entCompany.Text = string.Empty;
                    taps = 0;
                }
            }
            else
            {
                await DisplayAlert("", "Check the Internet connection.", "Ok");
                taps = 0;
            }
        }
    }
}