using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.ViewModels;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickListTemplate : Grid
    {
        public PickListTemplate()
        {
            InitializeComponent();

            var pickList = DataManager.GetPickLists();
            BindingContext = new PickListViewModel(pickList);
        }

        public async void Move()
        {
            await ContentContainer.TranslateTo(0, -300, 30, Easing.Linear);
            await Header.TranslateTo(0, -300, 30, Easing.Linear);
            Header.IsVisible = true;
            ContentContainer.IsVisible = true;
            await Header.TranslateTo(0, 0, 500);
            await ContentContainer.TranslateTo(0, 0, 300);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Application.Current.MainPage.DisplayAlert("", "Scanner", "Ok");
        }

        private async void PicklistContentPage(object sender, EventArgs e)
        {            
            await Navigation.PushModalAsync(new PicklistItems((int)((TappedEventArgs)e).Parameter));
        }

    }
}