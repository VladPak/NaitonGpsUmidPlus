using NaitonGps.Models;
using NaitonGps.Services;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace NaitonGps.Helpers
{
    public static class DataManager
    {
        #region Pick list

        public static List<PickListItem> GetPickListItems(int pickListId)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklistracks");                
                command.Parameters.Add("_picklistids", PgsqlDbType.Integer | PgsqlDbType.Array, new int[] { pickListId });                
                command.Parameters.Add("_limit", PgsqlDbType.Integer, 100);                

                string result = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, PickListItem[]>>(result);

                var pickListItems = dict.First().Value.ToList();
                return pickListItems;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public static List<PickList> GetPickLists()
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklists");
                command.Parameters.Add("_picklistid", PgsqlDbType.Integer);
                command.Parameters.Add("_statusid", PgsqlDbType.Integer,3);
        //command.Parameters.Add("_pickerid", PgsqlDbType.Integer,_user.PersonId);
        command.Parameters.Add("_pickerid", PgsqlDbType.Integer, AuthenticationService.User.PersonId);

        string result = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, PickList[]>>(result);

                var pickList = dict.First().Value.ToList();

                return pickList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static List<Rack> GetPickRacks(int deliveryOrderDetailsId, decimal quantity)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpickracksformobile");
                command.Parameters.Add("_deliveryorderdetailsid", PgsqlDbType.Integer, deliveryOrderDetailsId);
                
                string result = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, Rack[]>>(result);

                var rackList = dict.First().Value.ToList();
                
                return rackList.Where(x=>x.QuantityInStock>=quantity).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion Pick list

        #region Account 

        public static Roles[] GetRoles(int roleId)
        {
            SimpleWSA.Command command = new SimpleWSA.Command("rolemanager_getcheckroleobjects");
            command.Parameters.Add("_roleid", PgsqlDbType.Integer).Value = roleId;
            string result = SimpleWSA.Command.Execute(command,
                                                RoutineType.DataSet,
                                                httpMethod: SimpleWSA.HttpMethod.GET,
                                                responseFormat: ResponseFormat.JSON);

            var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, Roles[]>>(result);
            var allRoles = dataFinalize.Values.ToList();
            var mobile = allRoles.SelectMany(i=>i).Where(x => x.ObjectTypeId == 2 && x.TypeId == 6).ToArray();

            return mobile;
        }

        #endregion Account
    }
}
