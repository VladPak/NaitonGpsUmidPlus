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
                Preferences.Set("loginCompany", entCompany.Text);
                //Preferences.Set("loginEmail", entEmail.Text);
                
                //Call Web service
                taps++;
                var response = await ApiService.GetWebService(entCompany.Text);

                if (response)
                {
                    if (taps == 1)
                    {
                        //Login with email and password
                        //await Navigation.PushModalAsync(new LoginEmailScreen(), true);

                        var userEmail = entEmail.Text;
                        var userPassword = entPassword.Text;
                        Preferences.Set("loginEmail", entEmail.Text);
                        Preferences.Set("loginPassword", entPassword.Text);

                        var emailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                        if (CrossConnectivity.Current.IsConnected)
                        {
                            if (string.IsNullOrEmpty(userPassword) || string.IsNullOrWhiteSpace(userPassword))
                            {
                                await DisplayAlert("", "Invalid password", "Ok");
                                entCompany.Text = Preferences.Get("loginCompany", string.Empty);
                                entEmail.Text = Preferences.Get("loginEmail", string.Empty);

                                entPassword.Text = string.Empty;
                                entPassword.Focus();
                            }
                            else if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrEmpty(userEmail) || !Regex.IsMatch(userEmail, emailPattern))
                            {
                                await DisplayAlert("", "Invalid email", "Ok");
                                entCompany.Text = Preferences.Get("loginCompany", string.Empty);
                                entPassword.Text = Preferences.Get("loginPassword", string.Empty);

                                entEmail.Text = string.Empty;
                                entEmail.Focus();
                            }
                            else
                            {
                                while (true)
                                {
                                    try
                                    {
                                        string currentAppVersion = VersionTracking.CurrentVersion;

                                        Session session = new Session(userEmail,
                                                                        entPassword.Text,
                                                                        false,
                                                                        4,
                                                                        currentAppVersion,
                                                                        Preferences.Get("loginCompany", string.Empty),
                                                                        null);
                                        await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");

                                        Preferences.Set("token", SessionContext.Token);

                                        UserLoginDetails userLoginDetails = new UserLoginDetails
                                        {
                                            userEmail = SessionContext.Login,
                                            userPassword = SessionContext.Password,
                                            userToken = SessionContext.Token,
                                            appId = SessionContext.AppId,
                                            appVersion = SessionContext.AppVersion,
                                            isEncrypted = SessionContext.IsEncrypted,
                                            restServiceAddress = "https://connectionprovider.naiton.com/",
                                            domain = Preferences.Get("webservicelink", string.Empty)
                                        };


                                        App.Current.Properties["UserDetail"] = JsonConvert.SerializeObject(userLoginDetails);
                                        await App.Current.SavePropertiesAsync();
                                        Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                                        Application.Current.MainPage = new MainNavigationPage();
                                        break;
                                    }
                                    catch (RestServiceException exRes)
                                    {
                                        if (exRes.Code == "MI008")
                                        {
                                            await SessionContext.Refresh();

                                            continue;
                                        }
                                        else
                                        {
                                            await DisplayAlert("", exRes.Message, "Ok");
                                            entEmail.Text = string.Empty;
                                            entPassword.Text = string.Empty;
                                            entEmail.Focus();
                                            break;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        await DisplayAlert("", ex.Message, "Ok");
                                        entEmail.Text = string.Empty;
                                        entPassword.Text = string.Empty;
                                        entEmail.Focus();
                                        break;
                                    }
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
                    await DisplayAlert("", "Wrong company name.Please enter valid name", "Ok");
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