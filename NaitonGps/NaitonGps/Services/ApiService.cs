using NaitonGps.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NaitonGps.Services
{
    public static class ApiService
    {

    public void Test()
    {
      Console.WriteLine("hello github!!!");
    }


    public void Test13()
    {
      Console.WriteLine("hello github test13!!!");
    }

    public static async Task<bool> GetWebService(string webserviceLink)
        {
            string webservice = String.Format("https://connectionprovider.naiton.com/DataAccess/{0}/restservice/address", webserviceLink);

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(webservice);
                var responseContent = await response.Content.ReadAsStringAsync();
                var rsToString = responseContent.ToString();
            
                Preferences.Set("webservicelink", rsToString);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }

        public class WebServiceSuccessResponse<TSuccess, TError>
        {
            public TSuccess Success { get; set; }

            public TError Error { get; set; }
        }

        public class WebServiceSuccessResponse<TSuccess> : WebServiceSuccessResponse<TSuccess, WebServiceErrorContent>
        { }

        public class WebServiceErrorContent
        {
            public string Message { get; set; }
            public string ErrorCode { get; set; }
        }

        public class InitializeSessionResponseContent
        {
            public string Function { get; set; }
            public string Token { get; set; }
        }
    }
}
