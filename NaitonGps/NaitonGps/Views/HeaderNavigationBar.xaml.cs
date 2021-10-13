using Rg.Plugins.Popup.Extensions;
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
	public partial class HeaderNavigationBar : Grid
	{

		public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
		public static bool IsSmallScreen { get; } = ScreenWidth <= 360;
		public static bool IsBigScreen { get; } = ScreenWidth >= 360;


		public HeaderNavigationBar ()
		{
			InitializeComponent ();

			if (IsSmallScreen)
			{
				iconUser.HeightRequest = 25;
				iconUser.WidthRequest = 25;
				iconNotification.WidthRequest = 25;
				iconNotification.HeightRequest = 25;
			}
			else if (IsBigScreen)
			{
				iconUser.HeightRequest = 30;
				iconUser.WidthRequest = 30;
				iconNotification.HeightRequest = 30;
				iconNotification.WidthRequest = 30;
			}
		}

        private async void UserInfo(object sender, EventArgs e)
        {
			await Navigation.PushPopupAsync(new UserInformationPage());
		}

		private async void Notifications(object sender, EventArgs e)
        {
			await Navigation.PushPopupAsync(new NotificationsPage());
		}
	}
}