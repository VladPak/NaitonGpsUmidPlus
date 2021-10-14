using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.ViewsForEachRole.SalesExecutive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesExThirdPage : Grid
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        public SalesExThirdPage()
        {
            InitializeComponent();

            if (IsSmallScreen)
            {
                mainGrid.Margin = new Thickness(5, 20, 5, 15);
            }
            else if (IsBigScreen)
            {
                mainGrid.Margin = new Thickness(10, 0, 10, 10);

            }
        }
    }
}