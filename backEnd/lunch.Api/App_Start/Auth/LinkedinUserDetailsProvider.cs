using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using lunch.Api.Models.Account;
using lunch.BusinessLogic.Security;
using lunch.Configuration;
using Newtonsoft.Json.Linq;

namespace lunch.Api.Auth
{
    internal static class LinkedinUserDetailsProvider
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LinkedinUserDetailsProvider));

        private const string AccessTokenUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
        private const string UserInfoBaseUrl = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,email-address,picture-url)";
        
        public static async Task<ExternalUserDetails> GetUserDetails(LoginLinkedinModel model, CancellationToken cancellationToken)
        {
            if (model == null) return null;

            HttpClient httpClient = null;
            try
            {
                httpClient = CreateHttpClient();

                //1. get the access token
                var responseMessage = await httpClient.PostAsync(AccessTokenUrl,
                    new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                        new KeyValuePair<string, string>("code", model.Code),
                        new KeyValuePair<string, string>("redirect_uri", model.RedirectUri),
                        new KeyValuePair<string, string>("client_id", model.ClientId),
                        new KeyValuePair<string, string>("client_secret", ApplicationSettings.LinkedinClientSecret)
                    }), cancellationToken);
                responseMessage.EnsureSuccessStatusCode();

                var jObject = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
                var accessToken = jObject.GetStringValue("access_token");

                //2. get user info
                var request = new HttpRequestMessage(HttpMethod.Get, UserInfoBaseUrl + "?format=json&oauth2_access_token=" + Uri.EscapeDataString(accessToken));

                responseMessage = await httpClient.SendAsync(request, cancellationToken);
                responseMessage.EnsureSuccessStatusCode();

                jObject = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());

                var result = new ExternalUserDetails
                {
                    Id = jObject.GetStringValue("id"),
                    Email = jObject.GetStringValue("emailAddress"),
                    FirstName = jObject.GetStringValue("firstName"),
                    LastName = jObject.GetStringValue("lastName"),
                    DisplayName = jObject.GetStringValue("formattedName"),
                    PictureUrl = jObject.GetStringValue("pictureUrl")
                };
                
                return result;
            }
            catch (Exception e)
            {
                Log.Error("Could not fetch Linkedin user details.", e);
                return null;
            }
            finally
            {
                if (httpClient != null)
                    httpClient.Dispose();
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var result = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(60),
                MaxResponseContentBufferSize = 10485760L
            };

            result.DefaultRequestHeaders.ExpectContinue = false;

            return result;
        }

        private static string GetStringValue(this JObject user, string propertyName)
        {
            JToken jtoken;
            if (!user.TryGetValue(propertyName, out jtoken))
                return null;
            return jtoken.ToString();
        }
    }
}