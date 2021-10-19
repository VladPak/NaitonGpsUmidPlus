using NaitonGps.Helpers;
using NaitonGps.Models;
using NaitonGps.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistContentEdit : ContentPage
    {
        private readonly int pListId;
        private readonly List<PickListItem> items;
        public string modeResult = Preferences.Get("userMode", string.Empty);

        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 408;


        public PicklistContentEdit(int pickListId)
        {            
            pListId = pickListId;
            items= DataManager.GetPickListItems(pListId);
            //CloseAllPopup();
            InitializeComponent();

            if (IsSmallScreen)
            {
                mainGrid.Margin = new Thickness(10,15,10,5);
                rowToChange.Height = new GridLength(1.4, GridUnitType.Star);
                
                imgBack.HeightRequest = 25;
                imgBack.WidthRequest = 25;
                lblPicklist.FontSize = 18;

              
                lblChange.FontSize = 16;
            }
            else if (IsBigScreen)
            {
                mainGrid.Margin = new Thickness(10, 0,10,0);
                rowToChange.Height = new GridLength(1.5, GridUnitType.Star);

                imgBack.HeightRequest = 30;
                imgBack.WidthRequest = 30;

                lblPicklist.FontSize = 22;

                lblChange.FontSize = 20;

            }

            BindingContext = new PicklistContentDataViewModel(items);

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
         
        }

        public async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void ChangeQuantity(object sender, EventArgs e)
        {
            int id= (int)((TappedEventArgs)e).Parameter;
            var item = items.FirstOrDefault(x => x.PickListOrderDetailsId == id);
            await PopupNavigation.Instance.PushAsync(new PicklistQuantityBottomPopup(ref item));
        }

        private async void ShowRackList(object sender, EventArgs e)
        {            
            int id = (int)((TappedEventArgs)e).Parameter;
            var item = items.FirstOrDefault(x => x.PickListOrderDetailsId == id);
            await PopupNavigation.Instance.PushAsync(new PicklistSearchItemBottomPopup(ref item));
        }
    }
}