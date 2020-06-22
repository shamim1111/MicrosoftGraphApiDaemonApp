using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MicrosoftGraphApiDaemonApp.AuthenticationRepository
{
    public class AuthenticationIntegration : IAuthenticationRepository
    {
        public string instanceUrl { get; set; }
        public string graphApiUrl { get; set; }
        public string azureADtenantId { get; set; }
        public string graphApiAccessClientId { get; set; }
        public string azureKeyVaultAccessClientid { get; set; }
        public string azureKeyVaultUrl { get; set; }
        public string vaultBaseUrl { get; set; }
        public string keyVault_SecretName { get; set; }

        /// <summary>
        /// This method will return AuthenticationIntegration object values based on the appsettings.json file.
        /// </summary>
        /// <param name="appsettingsJsonFile"></param>
        /// <returns></returns>
        public AuthenticationIntegration GetAuthenticationDetails(string appsettingsJsonFile)
        {
            IConfigurationRoot config;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appsettingsJsonFile);
            config = builder.Build();
            return config.Get<AuthenticationIntegration>();
            
        }
    }
}
