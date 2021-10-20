using NaitonGps.Models;
using NaitonGps.ViewModels;
using NaitonGps.ViewsForEachRole;
using NaitonGps.ViewsForEachRole.Driver;
using NaitonGps.ViewsForEachRole.SalesExecutive;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleWSA;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NaitonGps.Services;
using System.IO;
using MoreLinq.Extensions;
using NaitonGps.Helpers;
using PhantomLib.Extensions;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPage : ContentPage
    {
        //Menu construction props
        readonly ControlTemplate defaultTemp = new ControlTemplate(typeof(DefaultTemplate));
        public int maxIndex;
        public int minIndex = 1;
        private int selectedIndex;
        public int maxNavItemsFull;
        public int maxNavItemsRemained;
        public ScreenTemplatesViewModel Screens;
        Image[] allNavItems;
        public Image linkToRemember;
        public List<Screens> res;

        //Screen size consideration for UI layout
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;


        public MainNavigationPage()
        {
            InitializeComponent();
            Screens = new ScreenTemplatesViewModel();
            allNavItems = new Image[] { navItem1, navItem2, navItem3, navItem4, navItem5 };

            if (IsSmallScreen)
            {
                bottomNavMenu.ColumnSpacing = 30;
            }
            else if (IsBigScreen)
            {
                bottomNavMenu.ColumnSpacing = 40;
            }
            selectedIndex = 1;
            SetMenuItems();


            if (maxIndex <= 1)
            {
                btnLeftArrow.IsVisible = false;
                btnRightArrow.IsVisible = false;
            }
            else
            {
                btnRightArrow.IsVisible = true;
                btnLeftArrow.IsVisible = true;
            }

            //When app is launched L/D mode
            if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Light)
            {
                Images.SetImageColor(allNavItems[0],Color.Green);
                var allButFirst = allNavItems.Skip(1).Take(4).ToArray();
                foreach (var item in allButFirst)
                {
                    Images.SetImageColor(item, Color.FromHex("#69717E"));
                }
            }
            else if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                Images.SetImageColor(allNavItems[0], Color.Green);                
                var allButFirst = allNavItems.Skip(1).Take(4).ToArray();
                foreach (var item in allButFirst)
                {
                    Images.SetImageColor(item, Color.White);
                }
            }

            //Coloring the navbar btns after switching the display mode light/dark
            Xamarin.Forms.Application.Current.RequestedThemeChanged += (s, a) =>
            {
                switch (a.RequestedTheme)
                {
                    case OSAppTheme.Dark:

                        foreach (var item in allNavItems)
                        {
                            if (Images.GetImageColor(item) == Color.FromHex("#69717E"))
                            {
                                Images.SetImageColor(item, Color.White);
                            }
                        }
                        break;
                    case OSAppTheme.Light:

                        foreach (var item in allNavItems)
                        {
                            if (Images.GetImageColor(item) == Color.White)
                            {
                                Images.SetImageColor(item, Color.FromHex("#69717E"));
                            }
                        }
                        break;
                    case OSAppTheme.Unspecified:
                        DisplayAlert("", "Unspecified OS Theme", "Ok");
                        break;
                }
            };
        }

        public async void SetMenuItems()
        {
            bool isLoggedIn = Xamarin.Forms.Application.Current.Properties.ContainsKey("IsLoggedIn") && Convert.ToBoolean(Application.Current.Properties["IsLoggedIn"]);

            if (!isLoggedIn)
            {
                await DisplayAlert("", "Oops access denied. (MainNavigationPage)", "Ok");
                Xamarin.Forms.Application.Current.Quit();
            }
            else
            {
                while (true)
                {
                    try
                    {
            var allRoles = DataManager.GetRoles(AuthenticationService.User.RoleId);


            //Get number of screens allowed for user (21)
            int numOfScreens = allRoles.Count();
                            //Sort the received screens with the existing list
                            var countScreens = Screens.Screens.Count();
                            res = Screens.Screens.Where(screen => allRoles.Any(title => title.Object.Equals(screen.ScreenTitle))).ToList();

                            //Count the sorted list of screens
                            var resC = res.Count();

                            if (resC > 0)
                            {
                                int numOfIndeces = resC / 5;
                                int numOfIndecesRemainder = resC % 5;

                                if (numOfIndecesRemainder > 0)
                                {
                                    maxIndex = numOfIndeces + 1;
                                }
                                else if (numOfIndecesRemainder == 0)
                                {
                                    maxIndex = numOfIndeces;
                                }

                                Array.Resize(ref allNavItems, 5);
                                allNavItems[0] = navItem1;
                                allNavItems[1] = navItem2;
                                allNavItems[2] = navItem3;
                                allNavItems[3] = navItem4;
                                allNavItems[4] = navItem5;

                                allNavItems[0].IsVisible = true;
                                allNavItems[1].IsVisible = true;
                                allNavItems[2].IsVisible = true;
                                allNavItems[3].IsVisible = true;
                                allNavItems[4].IsVisible = true;

                                //Divide filtered screens into batches (5 items per batch)
                                var calculatedBatches = res.Batch(5).ToList();

                                switch (selectedIndex)
                                {
                                    case 1:
                                        var index = 0;
                                        foreach (var sublist in calculatedBatches)
                                        {
                                            for (int i = 0; i < sublist.ToArray().Count(); i++)
                                            {
                                                if (sublist.Count() != allNavItems.Length)
                                                {
                                                    switch (sublist.Count())
                                                    {
                                                        case 1:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = false;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 2:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 3:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 4:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 5:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = true;
                                                            break;
                                                        default:
                                                            _ = DisplayAlert("", "Oops smth wront!", "Ok");
                                                            break;
                                                    }
                                                    int skipMenuItems = sublist.Count();
                                                    Array.Resize(ref allNavItems, skipMenuItems);

                                                    //allNavItems.Slice(0, 2);
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                            }
                                            index++;
                                            if (index > 0)
                                            {
                                                ControlTemplate = sublist.ToList()[0].ScreenLink;
                                                break;
                                            }
                                        }
                                        break;
                                    case 2:
                                        var index2 = 0;
                                        foreach (var sublist in calculatedBatches.Skip(1))
                                        {
                                            for (int i = 0; i < sublist.ToArray().Count(); i++)
                                            {
                                                if (sublist.Count() != allNavItems.Length)
                                                {
                                                    switch (sublist.Count())
                                                    {
                                                        case 1:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = false;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 2:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 3:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 4:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 5:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = true;
                                                            break;
                                                        default:
                                                            _ = DisplayAlert("", "Oops smth wront!", "Ok");
                                                            break;
                                                    }
                                                    int skipMenuItems = sublist.Count();
                                                    Array.Resize(ref allNavItems, skipMenuItems);

                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                            }
                                            index2++;
                                            if (index2 > 0)
                                            {
                                                ControlTemplate = sublist.ToList()[0].ScreenLink;
                                                break;
                                            }
                                        }
                                        break;
                                    case 3:
                                        var index3 = 0;
                                        foreach (var sublist in calculatedBatches.Skip(2))
                                        {
                                            for (int i = 0; i < sublist.ToArray().Count(); i++)
                                            {
                                                if (sublist.Count() != allNavItems.Length)
                                                {
                                                    switch (sublist.Count())
                                                    {
                                                        case 1:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = false;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 2:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 3:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 4:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 5:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = true;
                                                            break;
                                                        default:
                                                            _ = DisplayAlert("", "Oops smth wront!", "Ok");
                                                            break;
                                                    }
                                                    int skipMenuItems = sublist.Count();
                                                    Array.Resize(ref allNavItems, skipMenuItems);

                                                    //allNavItems.Slice(0, 2);
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                            }
                                            index3++;
                                            if (index3 > 0)
                                            {
                                                ControlTemplate = sublist.ToList()[0].ScreenLink;
                                                break;
                                            }
                                        }
                                        break;
                                    case 4:
                                        var index4 = 0;
                                        foreach (var sublist in calculatedBatches.Skip(3))
                                        {
                                            for (int i = 0; i < sublist.ToArray().Count(); i++)
                                            {
                                                if (sublist.Count() != allNavItems.Length)
                                                {
                                                    switch (sublist.Count())
                                                    {
                                                        case 1:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = false;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 2:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 3:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 4:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 5:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = true;
                                                            break;
                                                        default:
                                                            _ = DisplayAlert("", "Oops smth wront!", "Ok");
                                                            break;
                                                    }
                                                    int skipMenuItems = sublist.Count();
                                                    Array.Resize(ref allNavItems, skipMenuItems);

                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                            }
                                            index4++;
                                            if (index4 > 0)
                                            {
                                                ControlTemplate = sublist.ToList()[0].ScreenLink;
                                                break;
                                            }
                                        }
                                        break;
                                    case 5:
                                        var index5 = 0;
                                        foreach (var sublist in calculatedBatches.Skip(4))
                                        {
                                            for (int i = 0; i < sublist.ToArray().Count(); i++)
                                            {
                                                if (sublist.Count() != allNavItems.Length)
                                                {
                                                    switch (sublist.Count())
                                                    {
                                                        case 1:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = false;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 2:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = false;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 3:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = false;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 4:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = false;
                                                            break;
                                                        case 5:
                                                            allNavItems[0].IsVisible = true;
                                                            allNavItems[1].IsVisible = true;
                                                            allNavItems[2].IsVisible = true;
                                                            allNavItems[3].IsVisible = true;
                                                            allNavItems[4].IsVisible = true;
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    int skipMenuItems = sublist.Count();
                                                    Array.Resize(ref allNavItems, skipMenuItems);

                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < allNavItems.Length; j++)
                                                    {
                                                        allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                    }
                                                }
                                            }
                                            index5++;
                                            if (index5 > 0)
                                            {
                                                ControlTemplate = sublist.ToList()[0].ScreenLink;
                                                break;
                                            }
                                        }
                                        break;

                                case 6:
                                    var index6 = 0;
                                    foreach (var sublist in calculatedBatches.Skip(5))
                                    {
                                        for (int i = 0; i < sublist.ToArray().Count(); i++)
                                        {
                                            if (sublist.Count() != allNavItems.Length)
                                            {
                                                switch (sublist.Count())
                                                {
                                                    case 1:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = false;
                                                        allNavItems[2].IsVisible = false;
                                                        allNavItems[3].IsVisible = false;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 2:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = false;
                                                        allNavItems[3].IsVisible = false;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 3:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = true;
                                                        allNavItems[3].IsVisible = false;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 4:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = true;
                                                        allNavItems[3].IsVisible = true;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 5:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = true;
                                                        allNavItems[3].IsVisible = true;
                                                        allNavItems[4].IsVisible = true;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                int skipMenuItems = sublist.Count();
                                                Array.Resize(ref allNavItems, skipMenuItems);

                                                for (int j = 0; j < allNavItems.Length; j++)
                                                {
                                                    allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                }
                                            }
                                            else
                                            {
                                                for (int j = 0; j < allNavItems.Length; j++)
                                                {
                                                    allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                }
                                            }
                                        }
                                        index6++;
                                        if (index6 > 0)
                                        {
                                            ControlTemplate = sublist.ToList()[0].ScreenLink;
                                            break;
                                        }
                                    }
                                    break;

                                case 7:
                                    var index7 = 0;
                                    foreach (var sublist in calculatedBatches.Skip(6))
                                    {
                                        for (int i = 0; i < sublist.ToArray().Count(); i++)
                                        {
                                            if (sublist.Count() != allNavItems.Length)
                                            {
                                                switch (sublist.Count())
                                                {
                                                    case 1:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = false;
                                                        allNavItems[2].IsVisible = false;
                                                        allNavItems[3].IsVisible = false;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 2:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = false;
                                                        allNavItems[3].IsVisible = false;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 3:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = true;
                                                        allNavItems[3].IsVisible = false;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 4:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = true;
                                                        allNavItems[3].IsVisible = true;
                                                        allNavItems[4].IsVisible = false;
                                                        break;
                                                    case 5:
                                                        allNavItems[0].IsVisible = true;
                                                        allNavItems[1].IsVisible = true;
                                                        allNavItems[2].IsVisible = true;
                                                        allNavItems[3].IsVisible = true;
                                                        allNavItems[4].IsVisible = true;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                int skipMenuItems = sublist.Count();
                                                Array.Resize(ref allNavItems, skipMenuItems);

                                                for (int j = 0; j < allNavItems.Length; j++)
                                                {
                                                    allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                }
                                            }
                                            else
                                            {
                                                for (int j = 0; j < allNavItems.Length; j++)
                                                {
                                                    allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
                                                }
                                            }
                                        }
                                        index7++;
                                        if (index7 > 0)
                                        {
                                            ControlTemplate = sublist.ToList()[0].ScreenLink;
                                            break;
                                        }
                                    }
                                    break;
                                default:
                                        _ = DisplayAlert("", "No attached index for the menu", "Ok");
                                        btnLeftArrow.IsVisible = false;
                                        btnRightArrow.IsVisible = false;
                                        break;
                                }
                            }
                            else
                            {
                                _ = DisplayAlert("", "No screens available for you. Please contact development team.", "Ok");
                                ControlTemplate = defaultTemp;
                                selectedIndex = 1;
                            }                        

                        break;
                    }
                    catch (RestServiceException ex)
                    {
                        if (ex.Code == "MI008")
                        {
                            await DisplayAlert("", "The session is refreshed", "Ok");
                            await SessionContext.Refresh();
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("", ex.Message, "Haha");
                        break;
                    }
                }

            }

            //SimpleWSA.Command command = new SimpleWSA.Command("rolemanager_getcheckroleobjects");
            //command.Parameters.Add("_roleid", PgsqlDbType.Integer).Value = 1;
            //command.WriteSchema = WriteSchema.TRUE;
            //string xmlResult = SimpleWSA.Command.Execute(command,
            //                                   RoutineType.DataSet,
            //                                   httpMethod: SimpleWSA.HttpMethod.GET,
            //                                   responseFormat: ResponseFormat.JSON);

            //var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, Roles[]>>(xmlResult);
            //var allRoles = dataFinalize.Values.ToList();

            //foreach (var item in allRoles)
            //{
            //    //Get number of screens allowed for user (21)
            //    int numOfScreens = item.Select(p => p.Object).Count();
            //    //Sort the received screens with the existing list
            //    var countScreens = Screens.screens.Count();
            //    res = Screens.screens.Where(screen => item.Any(title => title.Object.Equals(screen.ScreenTitle))).ToList();

            //    //Count the sorted list of screens
            //    var resC = res.Count();

            //    if (resC > 0)
            //    {
            //        int numOfIndeces = resC / 5;
            //        int numOfIndecesRemainder = resC % 5;

            //        if (numOfIndecesRemainder > 0)
            //        {
            //            maxIndex = numOfIndeces + 1;
            //        }
            //        else if (numOfIndecesRemainder == 0)
            //        {
            //            maxIndex = numOfIndeces;
            //        }

            //        Array.Resize(ref allNavItems, 5);
            //        allNavItems[0] = navItem1;
            //        allNavItems[1] = navItem2;
            //        allNavItems[2] = navItem3;
            //        allNavItems[3] = navItem4;
            //        allNavItems[4] = navItem5;

            //        allNavItems[0].IsVisible = true;
            //        allNavItems[1].IsVisible = true;
            //        allNavItems[2].IsVisible = true;
            //        allNavItems[3].IsVisible = true;
            //        allNavItems[4].IsVisible = true;

            //        //Divide filtered screens into batches (5 items per batch)
            //        var calculatedBatches = res.Batch(5).ToList();

            //        switch (selectedIndex)
            //        {
            //            case 1:
            //                var index = 0;
            //                foreach (var sublist in calculatedBatches)
            //                {
            //                    for (int i = 0; i < sublist.ToArray().Count(); i++)
            //                    {
            //                        if (sublist.Count() != allNavItems.Length)
            //                        {
            //                            switch (sublist.Count())
            //                            {
            //                                case 1:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = false;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 2:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 3:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 4:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 5:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = true;
            //                                    break;
            //                                default:
            //                                    DisplayAlert("", "Oops smth wront!", "Ok");
            //                                    break;
            //                            }
            //                            int skipMenuItems = sublist.Count();
            //                            Array.Resize(ref allNavItems, skipMenuItems);

            //                            //allNavItems.Slice(0, 2);
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                    }
            //                    index++;
            //                    if (index > 0)
            //                    {
            //                        ControlTemplate = sublist.ToList()[0].ScreenLink;
            //                        break;
            //                    }
            //                }
            //                break;
            //            case 2:
            //                var index2 = 0;
            //                foreach (var sublist in calculatedBatches.Skip(1))
            //                {
            //                    for (int i = 0; i < sublist.ToArray().Count(); i++)
            //                    {
            //                        if (sublist.Count() != allNavItems.Length)
            //                        {
            //                            switch (sublist.Count())
            //                            {
            //                                case 1:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = false;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 2:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 3:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 4:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 5:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = true;
            //                                    break;
            //                                default:
            //                                    DisplayAlert("", "Oops smth wront!", "Ok");
            //                                    break;
            //                            }
            //                            int skipMenuItems = sublist.Count();
            //                            Array.Resize(ref allNavItems, skipMenuItems);

            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                    }
            //                    index2++;
            //                    if (index2 > 0)
            //                    {
            //                        ControlTemplate = sublist.ToList()[0].ScreenLink;
            //                        break;
            //                    }
            //                }
            //                break;
            //            case 3:
            //                var index3 = 0;
            //                foreach (var sublist in calculatedBatches.Skip(2))
            //                {
            //                    for (int i = 0; i < sublist.ToArray().Count(); i++)
            //                    {
            //                        if (sublist.Count() != allNavItems.Length)
            //                        {
            //                            switch (sublist.Count())
            //                            {
            //                                case 1:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = false;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 2:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 3:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 4:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 5:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = true;
            //                                    break;
            //                                default:
            //                                    DisplayAlert("", "Oops smth wront!", "Ok");
            //                                    break;
            //                            }
            //                            int skipMenuItems = sublist.Count();
            //                            Array.Resize(ref allNavItems, skipMenuItems);

            //                            //allNavItems.Slice(0, 2);
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                    }
            //                    index3++;
            //                    if (index3 > 0)
            //                    {
            //                        ControlTemplate = sublist.ToList()[0].ScreenLink;
            //                        break;
            //                    }
            //                }
            //                break;
            //            case 4:
            //                var index4 = 0;
            //                foreach (var sublist in calculatedBatches.Skip(3))
            //                {
            //                    for (int i = 0; i < sublist.ToArray().Count(); i++)
            //                    {
            //                        if (sublist.Count() != allNavItems.Length)
            //                        {
            //                            switch (sublist.Count())
            //                            {
            //                                case 1:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = false;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 2:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 3:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 4:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 5:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = true;
            //                                    break;
            //                                default:
            //                                    DisplayAlert("", "Oops smth wront!", "Ok");
            //                                    break;
            //                            }
            //                            int skipMenuItems = sublist.Count();
            //                            Array.Resize(ref allNavItems, skipMenuItems);

            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                    }
            //                    index4++;
            //                    if (index4 > 0)
            //                    {
            //                        ControlTemplate = sublist.ToList()[0].ScreenLink;
            //                        break;
            //                    }
            //                }
            //                break;
            //            case 5:
            //                var index5 = 0;
            //                foreach (var sublist in calculatedBatches.Skip(4))
            //                {
            //                    for (int i = 0; i < sublist.ToArray().Count(); i++)
            //                    {
            //                        if (sublist.Count() != allNavItems.Length)
            //                        {
            //                            switch (sublist.Count())
            //                            {
            //                                case 1:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = false;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 2:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = false;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 3:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = false;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 4:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = false;
            //                                    break;
            //                                case 5:
            //                                    allNavItems[0].IsVisible = true;
            //                                    allNavItems[1].IsVisible = true;
            //                                    allNavItems[2].IsVisible = true;
            //                                    allNavItems[3].IsVisible = true;
            //                                    allNavItems[4].IsVisible = true;
            //                                    break;
            //                                default:
            //                                    break;
            //                            }
            //                            int skipMenuItems = sublist.Count();
            //                            Array.Resize(ref allNavItems, skipMenuItems);

            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            for (int j = 0; j < allNavItems.Length; j++)
            //                            {
            //                                allNavItems[j].Source = sublist.ToList()[j].ScreenImage;
            //                            }
            //                        }
            //                    }
            //                    index5++;
            //                    if (index5 > 0)
            //                    {
            //                        ControlTemplate = sublist.ToList()[0].ScreenLink;
            //                        break;
            //                    }
            //                }
            //                break;
            //            default:
            //                DisplayAlert("", "No attached index for the menu", "Ok");
            //                btnLeftArrow.IsVisible = false;
            //                btnRightArrow.IsVisible = false;
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        DisplayAlert("", "No screens available for you. Please contact development team.", "Ok");
            //        ControlTemplate = defaultTemp;
            //        selectedIndex = 1;
            //    }
            //}
        }

        //Navigation controls
        //Previous Role (Swipe)
        private void SwipeGestureRecognizer_Swiped_PreviousRole(object sender, SwipedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                DisplayAlert("", "Swipe navigation currently unavailable. Use arrow buttons please.", "Ok");
                VibrationForNav();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                PreviousContent();
                SetMenuItems();
                MoveMenu();
                VibrationForNav();
            }
        }

        //Next Role (Swipe)
        private void SwipeGestureRecognizer_Swiped_NextRole(object sender, SwipedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                DisplayAlert("", "Swipe navigation currently unavailable. Use arrow buttons please.", "Ok");
                VibrationForNav();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                NextContent();
                SetMenuItems();
                MoveMenu();
                VibrationForNav();
            }
        }

        //Previous Role (Arrow button)
        private void TapGestureRecognizer_Tapped_PreviousRole(object sender, EventArgs e)
        {
            PreviousContent();
            SetMenuItems();
            MoveMenu();
            VibrationForNav();
        }

        //Next Role (Arrow button)
        private void TapGestureRecognizer_Tapped_NextRole(object sender, EventArgs e)
        {
            NextContent();
            SetMenuItems();
            MoveMenu();
            VibrationForNav();
        }

        //Navigation menu animation
        public async void MoveMenu()
        {
            await bottomNavMenu.TranslateTo(0, 200, 200, Easing.Linear);
            await bottomNavMenu.TranslateTo(0, 0);

            Image[] images = new Image[] { navItem1, navItem2, navItem3, navItem4, navItem5 };
            for (int i = 0; i < images.Length; i++)
            {
                if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Light)
                {
                    Images.SetImageColor(images[0], Color.Green);
                    Images.SetImageColor(images[i], Color.FromHex("#69717E"));
                }
                else if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark)
                {
                    Images.SetImageColor(images[0], Color.Green);
                    Images.SetImageColor(images[i], Color.White);
                }
            }
        }

        //Navigation functions
        public void NextContent()
        {
            selectedIndex++;
          
            if (selectedIndex > maxIndex)
            {
                selectedIndex = 1;
            }
        }

        public void PreviousContent()
        {
            selectedIndex--;
           
            if (selectedIndex < minIndex)
            {
                selectedIndex = maxIndex;
            }
        }

        private void NavigatingMenu(object sender, EventArgs e)
        {
            Image imgClick = sender as Image;
            int currentGridRowClicked = (int)imgClick.GetValue(Grid.ColumnProperty);

            var calculatedBatches = res.Batch(5).ToList();
            
            foreach (var imgToDefaultColor in allNavItems)
            {
                if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Light)
                {
                    Images.SetImageColor(imgToDefaultColor,Color.FromHex("#69717E"));
                }
                else if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark)
                {
                    Images.SetImageColor(imgToDefaultColor,Color.White);
                }
            }

            Images.SetImageColor(allNavItems[currentGridRowClicked],Color.Green);

            switch (selectedIndex)
            {
                case 1:
                    var firstBatch = calculatedBatches[0].ToArray();
                    ControlTemplate = firstBatch.ToList()[currentGridRowClicked].ScreenLink;
                    break;
                case 2:
                    var secondBatch = calculatedBatches[1].ToArray();
                    ControlTemplate = secondBatch.ToList()[currentGridRowClicked].ScreenLink;
                    break;
                case 3:
                    var thirdBatch = calculatedBatches[2].ToArray();
                    ControlTemplate = thirdBatch.ToList()[currentGridRowClicked].ScreenLink;
                    break;                
                case 4:
                    var fourthBatch = calculatedBatches[3].ToArray();
                    ControlTemplate = fourthBatch.ToList()[currentGridRowClicked].ScreenLink;
                    break;
                case 5:
                    var fifthBatch = calculatedBatches[4].ToArray();
                    ControlTemplate = fifthBatch.ToList()[currentGridRowClicked].ScreenLink;
                    break;
                default:
                    DisplayAlert("", "The page does not exist. Please contact with dev team.", "Ok");
                    break;
            }
        }

        public void VibrationForNav()
        {
            var duration = TimeSpan.FromMilliseconds(300);
            Vibration.Vibrate(duration);
        }
    }
}