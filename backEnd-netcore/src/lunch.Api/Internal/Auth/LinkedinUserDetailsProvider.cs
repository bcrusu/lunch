using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using lunch.Api.Models.Account;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using lunch.Configuration;
using Microsoft.Extensions.Logging;

namespace lunch.Api.Internal.Auth
{
    //TODO: review implementation & move to 'lunch.BusinessLogic' project
    internal class LinkedinUserDetailsProvider
    {
        private const string AccessTokenUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
        private const string UserInfoBaseUrl = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,email-address,headline,picture-urls::(original))";
        
        public static async Task<ExternalUserDetails> GetUserDetails(IServiceProvider serviceProvider, SignInLinkedinModel model, CancellationToken cancellationToken)
        {
            if (model == null) return null;

            var applicationSettings = serviceProvider.GetService<IApplicationSettings>();
            var logger = serviceProvider.GetService<ILogger<LinkedinUserDetailsProvider>>();

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
                        new KeyValuePair<string, string>("client_secret", applicationSettings.LinkedinClientSecret)
                    }), cancellationToken);
                responseMessage.EnsureSuccessStatusCode();

                var jObject = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
                var accessToken = GetStringValue(jObject, "access_token");

                //2. get user info
                var request = new HttpRequestMessage(HttpMethod.Get, UserInfoBaseUrl + "?format=json&oauth2_access_token=" + Uri.EscapeDataString(accessToken));

                responseMessage = await httpClient.SendAsync(request, cancellationToken);
                responseMessage.EnsureSuccessStatusCode();

                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                jObject = JObject.Parse(responseContent);

                var result = new ExternalUserDetails
                {
                    UserType = UserType.ExternalLinkedin,
                    Id = GetStringValue(jObject, "id"),
                    Email = GetStringValue(jObject, "emailAddress"),
                    FirstName = GetStringValue(jObject, "firstName"),
                    LastName = GetStringValue(jObject, "lastName"),
                    DisplayName = GetStringValue(jObject, "formattedName"),
                    Description = GetStringValue(jObject, "headline"),
                    PictureUrl = GetPictureUrl(jObject)
                };
                
                return result;
            }
            catch (Exception e)
            {
                logger.LogError("Could not fetch Linkedin user details. {0}", e);
                return null;
            }
            finally
            {
                if (httpClient != null)
                    httpClient.Dispose();
            }
        }

        private static string GetPictureUrl(JObject jObject)
        {
            var jtoken = jObject.SelectToken("$.pictureUrls.values[0]", false);
            return jtoken?.ToString();
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

        private static string GetStringValue(JObject jObject, string propertyName)
        {
            JToken jtoken;
            if (!jObject.TryGetValue(propertyName, out jtoken))
                return null;
            return jtoken.ToString();
        }
    }
}