using NaitonGps.Models;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaitonGps.Helpers
{
    public static class DataManager
    {
        private static UserLoginDetails _user;

        public static List<PickListItem> GetPickListItems(int pickListId)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getdeliveries");
                command.Parameters.Add("_deliveryid", PgsqlDbType.Integer | PgsqlDbType.Array, DBNull.Value);
                command.Parameters.Add("_productid", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_productname", PgsqlDbType.Varchar, "");
                command.Parameters.Add("_clientname", PgsqlDbType.Varchar, "");
                command.Parameters.Add("_clientid", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_companyname", PgsqlDbType.Varchar, "");
                command.Parameters.Add("_deliverystatus", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_dpdatefrom", PgsqlDbType.Timestamp, DBNull.Value);
                command.Parameters.Add("_dpdateto", PgsqlDbType.Timestamp, DBNull.Value);
                command.Parameters.Add("_shipmentfrom", PgsqlDbType.Timestamp, DBNull.Value);
                command.Parameters.Add("_shipmentto", PgsqlDbType.Timestamp, DBNull.Value);
                command.Parameters.Add("_businessids", PgsqlDbType.Integer | PgsqlDbType.Array, new int[] { 1 });
                command.Parameters.Add("_deliverycompany", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_picklistids", PgsqlDbType.Integer | PgsqlDbType.Array, new int[] { pickListId });
                command.Parameters.Add("_orderid", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_paymentmethodid", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_brandid", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_rackname", PgsqlDbType.Varchar, "");
                command.Parameters.Add("_stockid", PgsqlDbType.Integer, 1);
                command.Parameters.Add("_categoryid", PgsqlDbType.Integer, 0);
                command.Parameters.Add("_limit", PgsqlDbType.Integer, 100);

                command.WriteSchema = WriteSchema.TRUE;
                string xmlResult = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, PickListItem[]>>(xmlResult);

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
                command.Parameters.Add("_statusid", PgsqlDbType.Integer);
                command.WriteSchema = WriteSchema.TRUE;
                string xmlResult = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, PickList[]>>(xmlResult);

                var pickList = dict.First().Value.ToList();

                return pickList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
