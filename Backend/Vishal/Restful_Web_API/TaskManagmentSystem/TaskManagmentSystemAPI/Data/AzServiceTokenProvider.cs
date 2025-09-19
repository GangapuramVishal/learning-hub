using Azure.Core;
using Azure.Identity;
using System.Reflection.Metadata.Ecma335;
using static System.Net.WebRequestMethods;

namespace TaskManagmentSystemAPI.Data
{
    public class AzServiceTokenProvider
    {
        private static AccessToken accessToken;
        public static string GetAccessToken(IConfiguration configuration)
        {
            var tokencredential = new DefaultAzureCredential();
            accessToken = tokencredential.GetToken(
                new TokenRequestContext(scopes: new string[] { "https://database.windows.net" }, null, null, configuration["TenantId"]));
            return accessToken.Token;
        }


    }
}
