﻿using NaitonGps.Models;
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
        private static UserLoginDetails _user;

        #region Pick list

        public static List<PickListItem> GetPickListItems(int pickListId)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklistracks");                
                command.Parameters.Add("_picklistids", PgsqlDbType.Integer | PgsqlDbType.Array, new int[] { pickListId });                
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
                command.Parameters.Add("_statusid", PgsqlDbType.Integer,3);
                command.Parameters.Add("_pickerid", PgsqlDbType.Integer,_user.PersonId);
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

        public static List<Rack> GetPickRacks(int deliveryOrderDetailsId, decimal quantity)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpickracksformobile");
                command.Parameters.Add("_deliveryorderdetailsid", PgsqlDbType.Integer, deliveryOrderDetailsId);
                
                command.WriteSchema = WriteSchema.TRUE;
                string xmlResult = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, Rack[]>>(xmlResult);

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

        public static UserLoginDetails RegistrationServiceSession()
        {
            try
            {
                Preferences.Set("token", SessionContext.Token);

                UserLoginDetails userLoginDetails = new UserLoginDetails
                {
                    userEmail = SessionContext.Login,
                    userPassword = SessionContext.Password,
                    userToken = SessionContext.Token,
                    appId = SessionContext.AppId,
                    appVersion = SessionContext.AppVersion,
                    isEncrypted = SessionContext.IsEncrypted,
                    connectionProviderAddress = "https://connectionprovider.naiton.com/",
                    domain = SessionContext.Domain
                };
                _user = userLoginDetails;

                return userLoginDetails;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static void SetUserData(out int roleId)
        {
            roleId = 0;
            UserLoginDetails dataFinalizeUserEP = JsonConvert.DeserializeObject<UserLoginDetails>((string)Application.Current.Properties["UserDetail"]);
            var userid = dataFinalizeUserEP.PersonId;
            SimpleWSA.Command commandGetAllUserData = new SimpleWSA.Command("userlogin_checklogin5");
            commandGetAllUserData.Parameters.Add("_login", PgsqlDbType.Varchar).Value = dataFinalizeUserEP.userEmail;
            commandGetAllUserData.Parameters.Add("_password", PgsqlDbType.Varchar).Value = dataFinalizeUserEP.userPassword;
            commandGetAllUserData.WriteSchema = WriteSchema.TRUE;

            string xmlResult1 = SimpleWSA.Command.Execute(commandGetAllUserData,
                                                        RoutineType.DataSet,
                                                        httpMethod: SimpleWSA.HttpMethod.GET,
                                                        responseFormat: ResponseFormat.JSON);

            var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, UserDetails[]>>(xmlResult1);
            var newUser = dataFinalize.First().Value.First();

            if (_user is null)
                RegistrationServiceSession();

            _user.RoleId = newUser.EmployeeRightId;
            _user.PersonId = newUser.EmployeeId;            

            roleId = dataFinalize["userlogin_checklogin5"].Select(x => x.EmployeeRightId).First();
        }

        public static UserLoginDetails GetCurrentUser()
        {
            return _user;
        }

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
