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
		public HeaderNavigationBar ()
		{
			InitializeComponent ();
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