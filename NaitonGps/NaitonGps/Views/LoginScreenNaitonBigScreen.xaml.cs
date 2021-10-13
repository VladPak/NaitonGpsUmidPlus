using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.Services;
using Newtonsoft.Json;
using Plugin.Connectivity;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class LoginScreenNaitonBigScreen : ContentPage
    {
        int taps = 0;
        
        public LoginScreenNaitonBigScreen()
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

            scrollToActive.IsEnabled = false;
            imgLogo.TranslationY = -130;
            GridFrame.TranslationY = 700;

            if (Device.RuntimePlatform == Device.iOS)
            {
                //ScrollViewMain.IsEnabled = true;
                Grid.SetRowSpan(frameLogin, 2);
                Grid.SetRow(GridFrame, 3);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                //ScrollViewMain.IsEnabled = false;
                //Grid.SetRowSpan(frameLogin, 3);
                //Grid.SetRow(GridFrame, 2);
            }
        }

        uint duration = 250;
        double openY = (Device.RuntimePlatform == "Android") ? 120 : 60;
        double openYImgLogo = (Device.RuntimePlatform == "Android") ? -280 : 60;


        //Tap the background to open Login frame
        private async void PopUpLoginFrame(object sender, EventArgs e)
        {
            if (Backdrop.Opacity == 0)
            {
                await OpenDrawer();
                scrollToActive.IsEnabled = true;

            }
            else
            {
                await CloseDrawer();
            }
        }

        //Tap to appeared boxview to close Login frame
        private async void ClickToCloseDrawer(object sender, EventArgs e)
        {
            if (isBackdropTapEnabled)
            {
                await CloseDrawer();
            }
        }

        double lastPanY = 0;
        bool isBackdropTapEnabled = true;

        //Sliding up/down Login frame
        async void PanGestureRecognizer_PanUpdated(System.Object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                isBackdropTapEnabled = false;
                lastPanY = e.TotalY;
                Debug.WriteLine($"Running: {e.TotalY}");
                if (e.TotalY > 0)
                {
                    GridFrame.TranslationY = openY + e.TotalY;
                }

            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                //Debug.WriteLine($"Completed: {e.TotalY}");
                if (lastPanY < 110)
                {
                    await OpenDrawer();
                    scrollToActive.IsEnabled = true;

                }
                else
                {
                    await CloseDrawer();
                }
                isBackdropTapEnabled = true;
            }
        }

        //Login
        private async void LoginInit(object sender, EventArgs e)
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
                                                                        userPassword,
                                                                        false,
                                                                        4,
                                                                        currentAppVersion,
                                                                        Preferences.Get("loginCompany", string.Empty),
                                                                        null);
                                        await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");

                                        var user = DataManager.RegistrationServiceSession();
                                        
                                        Application.Current.Properties["UserDetail"] = JsonConvert.SerializeObject(user);
                                        await Application.Current.SavePropertiesAsync();
                                        Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.TrueString;

                                        Xamarin.Forms.Application.Current.MainPage = new MainNavigationPage();
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

        //Call for TermsAndServices
        private async void CallTermsAndService(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermsAndServices());
        }
        
        //Call for NeedHelp
        private async void CallNeedHelp(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NeedHelp());
        }


        async Task OpenDrawer()
        {
            await Task.WhenAll
            (
                Backdrop.FadeTo(1, length: duration),
                GridFrame.TranslateTo(0, openY, length: duration, easing: Easing.SinIn),
                imgLogo.TranslateTo(0, openYImgLogo, length: duration, easing: Easing.SinIn)
            );
        }

        async Task CloseDrawer()
        {
            await Task.WhenAll
            (
                Backdrop.FadeTo(0, length: duration),
                GridFrame.TranslateTo(0, 700, length: duration, easing: Easing.SinIn),
                imgLogo.TranslateTo(0, -130, length: duration, easing: Easing.SinIn)
            );
        }

    }
}