using Microsoft.Graph;
using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using MicrosoftGraphApiDaemonApp.GraphApiRepository.TokenRepository;
using MicrosoftGraphApiDaemonApp.GraphApiRepository.UserRepository;
using MicrosoftGraphApiDaemonApp.KeyVaultAccessRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Graph Api Daemon Application");
            //Get the AuthenticationIntegration instance
            IAuthenticationRepository iAuthenticationRepository = Factory.CreateAuthenticationIntegration();
            //Get the KeyVaultAccessImplementatation instance
            IKeyVaultAccessRepository iKeyVaultAccessRepository = Factory.CreateKeyVaultAccessImplementation();
            //Get the client secrect from azure key vault to access Graph api app
            Task<string> keyVaultSecret = iKeyVaultAccessRepository.GetClientSecretFromKeyVault(iAuthenticationRepository);
            keyVaultSecret.Wait();
            //Get the TokenImplementation instance
            ITokenProcessorRepository iTokenProcessorRepository = Factory.CreateTokenImplementation();
            //Get the AuthenticationIntegration instance
            IAuthenticationRepository iAuthRepository = Factory.CreateAuthenticationIntegration();
            Task<string> token = iTokenProcessorRepository.GetGraphApiAccessToken(iAuthRepository, keyVaultSecret.Result.ToString());
            token.Wait();
            IUserDetailsRepository iUserDetailsRepository = Factory.CreateUserImplementation();
            //Get the AuthenticationIntegration instance
            IAuthenticationRepository iARepository = Factory.CreateAuthenticationIntegration();
            Task<List<User>> users = iUserDetailsRepository.GetUserDetails(iARepository, token.Result.ToString());
            users.Wait();
            //Display the user principal name
            foreach(var user in users.Result)
            {
                Console.WriteLine($"User Principal Name:{user.UserPrincipalName}");

            }


            Console.ReadKey();
        }
    }
}
