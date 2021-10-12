using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistPrintLabelsBottomPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public int initialValue = 0;

        public PicklistPrintLabelsBottomPopup()
        {
            InitializeComponent();
            entQuantity.Text = initialValue.ToString();    
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await DisplayAlert("", "Save btn is clicked", "Ok");
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            int newValue = initialValue -= 1;
            if (newValue < 0)
            {
                DisplayAlert("", "Negative value is not accepted", "Ok");
                initialValue = 0;
                entQuantity.Text = initialValue.ToString();
            }
            else
            {
                entQuantity.Text = newValue.ToString();
            }
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            int newValue2 = initialValue += 1;
            if (newValue2 > 1000000000)
            {
                DisplayAlert("", "Max value is 1 billion units", "Ok");
            }
            else
            {
                entQuantity.Text = newValue2.ToString();
            }
        }
    }
}