﻿using NaitonGps.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.ViewsForEachRole
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FourthRoleTemplateFour : Grid
    {
        public FourthRoleTemplateFour()
        {
            InitializeComponent();
        }
        public async void Move()
        {
            await Task.Run(() => new FourthRoleTemplate2());
            //await Content.TranslateTo(0, -300, 30, Easing.Linear);
            //await Header.TranslateTo(0, -300, 30, Easing.Linear);
            //Header.IsVisible = true;
            //Content.IsVisible = true;
            //await Header.TranslateTo(0, 0, 500);
            //await Content.TranslateTo(0, 0, 300);
        }
    }
}