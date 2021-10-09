using NaitonGps.Models;
using NaitonGps.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NaitonGps.ViewModels
{
    public class UserViewModel
    {
        public ObservableCollection<Users> users { get; set; }
        public ObservableCollection<Screens> screens { get; set; }


        public UserViewModel()
        {
            users = new ObservableCollection<Users>
            {
                new Users
                {
                    userEmail = Preferences.Get("loginEmail", string.Empty), userRole = "Admin"
                },
            };

            screens = new ObservableCollection<Screens>
            {
                new Screens
                {
                    ScreenTitle = "AccountingReportsDiagnosticReports", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "AccountingReportsDiagnosticReports3", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "BalanceSheetForm", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "DublicateContentFormInternalTransport", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "InvalidEmailAddressesForm", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "NaitonTranslation", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "NewMainDelivery", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "OrderLogForm", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "PerformanceScreenForm", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReeleezeeForm", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "RentalOverviewForm", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum1", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum2", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum3", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum4", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum5", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum6", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum7", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum8", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
                new Screens
                {
                    ScreenTitle = "ReportMenuNum9", ScreenImage = "home.png", ScreenLink = new ControlTemplate(typeof(FirstRoleTemplate))
                },
            };
        }
    }
}
