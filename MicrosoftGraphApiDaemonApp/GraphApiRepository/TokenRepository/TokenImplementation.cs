using Microsoft.Identity.Client;
using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp.GraphApiRepository.TokenRepository
{
    public class TokenImplementation : ITokenProcessorRepository
    {
        /// <summary>
        /// This method uses client secret which was kept at azure key vault to get access token for second application.
        /// This token will be used to access microsoft graph APIs.
        /// </summary>
        /// <param name="_iAuthenticationRepository"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public async Task<string> GetGraphApiAccessToken(IAuthenticationRepository _iAuthenticationRepository, string clientSecret)
        {
           
            AuthenticationResult authResult = null;
            IAuthenticationRepository iAuthenticationRepository = _iAuthenticationRepository.GetAuthenticationDetails("appsettings.json");
            try
            {
               

                    string[] scope = { $"{iAuthenticationRepository.graphApiUrl}.default" };

                    IConfidentialClientApplication iCCA = ConfidentialClientApplicationBuilder.Create(iAuthenticationRepository.graphApiAccessClientId).
                    WithClientSecret(clientSecret)
                    .WithAuthority(string.Format(CultureInfo.InvariantCulture, iAuthenticationRepository.instanceUrl, iAuthenticationRepository.azureADtenantId)).Build();

                    if (authResult == null)
                    {
                        authResult = await iCCA.AcquireTokenForClient(scope).ExecuteAsync();
                    }
            }
            catch (MsalServiceException ex)
            {
                Console.WriteLine("Scope is not supported");

            }
            return authResult.AccessToken;
        }
    }
}
