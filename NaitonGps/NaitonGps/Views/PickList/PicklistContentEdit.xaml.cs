using NaitonGps.Helpers;
using NaitonGps.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistContentEdit : ContentPage
    {
        private readonly int pListId;
        public string modeResult = Preferences.Get("userMode", string.Empty);

        public PicklistContentEdit(int pickListId)
        {
            pListId = pickListId;
            CloseAllPopup();
            InitializeComponent();
            var pickListItems = DataManager.GetPickListItems(pickListId);
            BindingContext = new PicklistContentDataViewModel(pickListItems);

            switch (modeResult)
            {
                case "readOnly":
                    //gridToHide.IsVisible = false;
                    //Grid.SetRow(mainGrid, 2);
                    //mainGrid.Children.RemoveAt(2);

                    var row = Grid.GetRow(mainGrid.Children[2]);
                    var children = mainGrid.Children.ToList();
                    foreach (var child in children.Where(child => Grid.GetRow(child) == row))
                    {
                        mainGrid.Children.Remove(child);
                    }
                    foreach (var child in children.Where(child => Grid.GetRow(child) > row))
                    {
                        Grid.SetRow(child, Grid.GetRow(child) - 1);
                    }

                    mainGrid.RowDefinitions.RemoveAt(mainGrid.RowDefinitions.Count - 1);

                    break;
                case "readAndEdit":
                    gridToHide.IsVisible = true;
                    Grid.SetRow(mainGrid, 3);
                    break;
                default:
                    gridToHide.IsVisible = true;
                    Grid.SetRow(mainGrid, 3);
                    DisplayAlert("", "Server error. Please contact app development team", "Ok");
                    break;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistPrintLabelsBottomPopup());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await DisplayAlert("", "Save and Print", "Ok");            
        }

        public async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistQuantityBottomPopup());
        }

        private async void ShowRackList(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PicklistSearchItemBottomPopup((int)((TappedEventArgs)e).Parameter));
        }
    }
}