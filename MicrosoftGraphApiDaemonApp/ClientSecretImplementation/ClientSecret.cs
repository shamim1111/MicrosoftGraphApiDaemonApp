using MicrosoftGraphApiDaemonApp.ClientSecretRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MicrosoftGraphApiDaemonApp.ClientSecretImplementation
{
    public class ClientSecret : IClientSecretRepository
    {
        

        public string GetClientSecret()
        {
            return ConfigurationManager.AppSettings["clientSecret"].ToString();
        }
    }
}
