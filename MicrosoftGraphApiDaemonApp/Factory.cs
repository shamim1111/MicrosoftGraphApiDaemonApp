using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using MicrosoftGraphApiDaemonApp.ClientSecretImplementation;
using MicrosoftGraphApiDaemonApp.ClientSecretRepository;
using MicrosoftGraphApiDaemonApp.GraphApiRepository.TokenRepository;
using MicrosoftGraphApiDaemonApp.GraphApiRepository.UserRepository;
using MicrosoftGraphApiDaemonApp.KeyVaultAccessRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftGraphApiDaemonApp
{
    public static class Factory
    {
        public static IClientSecretRepository CreateClientSecret()
        {
            return new ClientSecret();
        }
        public static IAuthenticationRepository CreateAuthenticationIntegration()
        {
            return new AuthenticationIntegration();
        }
        public static IKeyVaultAccessRepository CreateKeyVaultAccessImplementation()
        {
            return new KeyVaultAccessImplementation(CreateClientSecret(), CreateAuthenticationIntegration());

        }
        public static ITokenProcessorRepository CreateTokenImplementation()
        {
            return new TokenImplementation();
        }
        public static IUserDetailsRepository CreateUserImplementation()
        {
            return new UserImplementaion();
        }
        
    }
}
