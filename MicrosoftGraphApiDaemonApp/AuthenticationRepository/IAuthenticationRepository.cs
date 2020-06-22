using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftGraphApiDaemonApp.AuthenticationRepository
{
    public interface IAuthenticationRepository
    {
       
        string instanceUrl { get; set; }
        string graphApiUrl { get; set; }
        string azureADtenantId { get; set; }
        string graphApiAccessClientId { get; set; }
        string azureKeyVaultAccessClientid { get; set; }
        string azureKeyVaultUrl { get; set; }
        string vaultBaseUrl { get; set; }
        string keyVault_SecretName { get; set; }
        
        AuthenticationIntegration GetAuthenticationDetails(string appsettingsJsonFile);
        
    }
}
