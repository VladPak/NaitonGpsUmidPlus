using NaitonGps.Models;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NaitonGps.Services
{
  public class AuthenticationService
  {
    public const string connectionProviderAddress = "https://connectionprovider.naiton.com";

    public static async Task<bool> IsCompanyNameValid(string companyName)
    {
      var httpClient = new HttpClient();
      var requestUri = connectionProviderAddress + $"/DataAccess/{companyName}/restservice/address";
      var response = await httpClient.GetAsync(requestUri);
      var responseContent = await response.Content.ReadAsStringAsync();
      return response.IsSuccessStatusCode;
    }

    public static async Task CreateSessionAndSaveUserDetail(string email, string password, int appId, string appVersion, string domain)
    {
      Session session = new Session(email, password, false, appId, appVersion, domain, null);
      string token = await session.CreateByConnectionProviderAddressAsync(connectionProviderAddress);

      user = new UserLoginDetails
      {
        userEmail = SessionContext.Login,
        userPassword = SessionContext.Password,
        appId = SessionContext.AppId,
        appVersion = SessionContext.AppVersion,
        isEncrypted = SessionContext.IsEncrypted,
        domain = SessionContext.Domain
      };

      CheckLogin(user);

      Application.Current.Properties["UserDetail"] = JsonConvert.SerializeObject(user);
    }

    public static async Task RestoreSession()
    {
      user = JsonConvert.DeserializeObject<UserLoginDetails>((string)Application.Current.Properties["UserDetail"]);
      Session session = new Session(user.userEmail,
                                    user.userPassword,
                                    user.isEncrypted,
                                    user.appId,
                                    user.appVersion,
                                    user.domain,
                                    null);
      await session.CreateByConnectionProviderAddressAsync(AuthenticationService.connectionProviderAddress);
    }

    public static void CheckLogin(UserLoginDetails userLoginDetails)
    {
      SimpleWSA.Command commandGetAllUserData = new SimpleWSA.Command("userlogin_checklogin5");
      commandGetAllUserData.Parameters.Add("_login", PgsqlDbType.Varchar).Value = userLoginDetails.userEmail;
      commandGetAllUserData.Parameters.Add("_password", PgsqlDbType.Varchar).Value = userLoginDetails.userPassword;

      string result = SimpleWSA.Command.Execute(commandGetAllUserData,
                                                RoutineType.DataSet,
                                                httpMethod: SimpleWSA.HttpMethod.GET,
                                                responseFormat: ResponseFormat.JSON);

      var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, UserDetails[]>>(result);
      var userDetails = dataFinalize.First().Value.First();

      userLoginDetails.RoleId = userDetails.EmployeeRightId;
      userLoginDetails.PersonId = userDetails.EmployeeId;
    }

    private static UserLoginDetails user = null;
    public static UserLoginDetails User
    {
      get
      {
        return user;
      }
    }
  }
}
