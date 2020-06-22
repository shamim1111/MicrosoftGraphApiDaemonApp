using Microsoft.Azure.KeyVault;
using Microsoft.Identity.Client;
using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using MicrosoftGraphApiDaemonApp.ClientSecretRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp.KeyVaultAccessRepository
{
    public class KeyVaultAccessImplementation:IKeyVaultAccessRepository
    {  
        IClientSecretRepository _iClientSecretRepository;
        IAuthenticationRepository _iAuthenticationRepository;
        public KeyVaultAccessImplementation(IClientSecretRepository iClientSecretRepository, IAuthenticationRepository iAuthenticationRepository)
        {
            _iClientSecretRepository = iClientSecretRepository;
            _iAuthenticationRepository = iAuthenticationRepository;

        }
        /// <summary>
        /// This method will authenticate with azure active directory and will get the client secrect to access microsoft 
        /// graph APIs
        /// </summary>
        /// <param name="iAuthRepository"></param>
        /// <returns></returns>
        public async Task <string>GetClientSecretFromKeyVault(IAuthenticationRepository iAuthRepository)
        {
            IAuthenticationRepository iAuthenticationRepository = iAuthRepository.GetAuthenticationDetails("appsettings.json");

            try
            {
                var keyVault = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessTokenToAuthenticateWithKeyVault));
                var secretBundle = await keyVault.GetSecretAsync(iAuthenticationRepository.vaultBaseUrl, iAuthenticationRepository.keyVault_SecretName);

                return secretBundle.Value;
            }
            catch(Exception ex)
            {
                return null;
            }

            

        }

       private  async Task<string> GetAccessTokenToAuthenticateWithKeyVault(string authority,string resoure,string scope)
        {
            KeyVaultClient kvClient = null;
            AuthenticationResult authResult = null;
            IAuthenticationRepository iAuthenticationRepository = _iAuthenticationRepository.GetAuthenticationDetails("appsettings.json");
            try
            {
                if (kvClient is null)
                {

                    string []scopeArray = {$"{iAuthenticationRepository.azureKeyVaultUrl}.default" };
                   
                    authority = string.Format(CultureInfo.InvariantCulture, iAuthenticationRepository.instanceUrl, iAuthenticationRepository.azureADtenantId);
                   
                        IConfidentialClientApplication iCCA = ConfidentialClientApplicationBuilder.Create(iAuthenticationRepository.azureKeyVaultAccessClientid).
                        WithClientSecret(_iClientSecretRepository.GetClientSecret())
                        .WithAuthority(authority).Build();

                        if (authResult == null)
                        {
                            authResult = await iCCA.AcquireTokenForClient(scopeArray).ExecuteAsync();
                        }



                        //if (authResult != null)
                        //    return authResult.AccessToken;
                        //else
                        //    return null;

                }
            }
            catch (Exception ex)
            {
                return null;

            }
            return authResult.AccessToken;

        }
    }
}
