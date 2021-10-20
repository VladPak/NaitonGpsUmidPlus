using NaitonGps.Models;
using NaitonGps.Services;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ContinueSession : ContentPage
  {
    public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
    public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
    public static bool IsBigScreen { get; } = ScreenWidth >= 480;

    public ContinueSession()
    {
      InitializeComponent();
      StartSession().GetAwaiter();
    }

    private async Task StartSession()
    {
      try
      {
        await AuthenticationService.RestoreSession();

        Application.Current.MainPage = new NavigationPage(new MainNavigationPage());
      }
      catch (RestServiceException rex)
      {
        Console.WriteLine(rex.ToString());
        await DisplayAlert("", "Cannot restore session from existing credentials, please press Log Out, and enter your credentials again", "Ok");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}