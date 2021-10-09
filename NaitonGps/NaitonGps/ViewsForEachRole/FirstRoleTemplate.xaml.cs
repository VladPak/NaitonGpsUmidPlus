using NaitonGps.Models;
using NaitonGps.Services;
using NaitonGps.ViewModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstRoleTemplate : Grid
    {
        public FirstRoleTemplate()
        {
            InitializeComponent();

            SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklists");
            command.Parameters.Add("_picklistid", PgsqlDbType.Integer);
            command.Parameters.Add("_statusid", PgsqlDbType.Integer);
            command.WriteSchema = WriteSchema.TRUE;
            string xmlResult = SimpleWSA.Command.Execute(command,
                                                RoutineType.DataSet,
                                                httpMethod: SimpleWSA.HttpMethod.GET,
                                                responseFormat: ResponseFormat.JSON);
            var dict = JsonConvert.DeserializeObject<Dictionary<string,PickList[]>>(xmlResult);

            var pickList = dict.First().Value.ToList();            

            BindingContext = new PickListViewModel(pickList);
        }

        public async void move()
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
            await App.Current.MainPage.DisplayAlert("", "Scanner", "Ok");
        }

        private async void PicklistContentPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PicklistContent());
        }

    }
}