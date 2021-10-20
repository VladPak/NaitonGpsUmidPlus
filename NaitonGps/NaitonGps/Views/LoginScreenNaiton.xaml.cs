using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.Services;
using Newtonsoft.Json;
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
  public partial class LoginScreenNaiton : ContentPage
  {
    int taps = 0;

    public LoginScreenNaiton()
    {
      InitializeComponent();

      entCompany.Text = string.Empty;
      entEmail.Text = string.Empty;
      entPassword.Text = string.Empty;

#if DEBUG
      entCompany.Text = "upstairstest";
      entEmail.Text = "m.aerts@upstairs.com";
      entPassword.Text = "Gromit12";
#endif

      scrollToActivate.IsEnabled = false;
      imgLogo.TranslationY = 100;
      frameLogin.TranslationY = 450;
    }

    private async void PopUpLoginFrame(object sender, EventArgs e)
    {
      await imgLogo.TranslateTo(0, -45, 280, Easing.Linear);
      await frameLogin.TranslateTo(0, 0, 330, Easing.Linear);
      scrollToActivate.IsEnabled = true;
    }

    private async void LoginInit(object sender, EventArgs e)
    {
      if (CrossConnectivity.Current.IsConnected)
      {
        taps++;

        var domain = entCompany.Text;
        bool isCompanyNameValid = await AuthenticationService.IsCompanyNameValid(domain);
        if (isCompanyNameValid)
        {
          if (taps == 1)
          {
            var email = entEmail.Text;
            var password = entPassword.Text;

            var emailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (CrossConnectivity.Current.IsConnected)
            {
              if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
              {
                await DisplayAlert("", "Invalid password", "Ok");
                entCompany.Text = domain;
                entEmail.Text = email;

                entPassword.Text = string.Empty;
                entPassword.Focus();
              }
              else if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email) || !Regex.IsMatch(email, emailPattern))
              {
                await DisplayAlert("", "Invalid email", "Ok");
                entCompany.Text = domain;
                entPassword.Text = password;

                entEmail.Text = string.Empty;
                entEmail.Focus();
              }
              else
              {
                int appId = 6;
                string appVersion = VersionTracking.CurrentVersion; ;
                try
                {
                  await AuthenticationService.CreateSessionAndSaveUserDetail(email, password, appId, appVersion, domain);

                  Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.TrueString;
                  Xamarin.Forms.Application.Current.MainPage = new MainNavigationPage();
                }
                catch (RestServiceException rex)
                {
                  await DisplayAlert("", rex.OriginalMessage, "Ok");
                  entEmail.Text = string.Empty;
                  entPassword.Text = string.Empty;
                  entEmail.Focus();
                }
                catch (Exception ex)
                {
                  await DisplayAlert("", ex.Message, "Ok");
                  entEmail.Text = string.Empty;
                  entPassword.Text = string.Empty;
                  entEmail.Focus();
                }
              }
            }
            else
            {
              await DisplayAlert("", "Check the Internet connection.", "Ok");
            }
            taps = 0;
          }
          else if (taps >= 2)
          {
            taps = 1;
            await DisplayAlert("", "Please wait. Your request is being proceeded", "Ok");
          }
        }
        else
        {
          await DisplayAlert("", "Wrong company name. Please enter valid name", "Ok");
          entCompany.Text = string.Empty;
          entCompany.Focus();
          taps = 0;
        }
      }
      else
      {
        await DisplayAlert("", "Check the Internet connection.", "Ok");
        taps = 0;
      }
    }

    private async void CallNeedHelp(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new NeedHelp());
    }

    private async void CallTermsAndService(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new TermsAndServices());
    }
  }
}