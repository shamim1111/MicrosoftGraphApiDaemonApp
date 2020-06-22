using Microsoft.Graph;
using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp.GraphApiRepository.UserRepository
{
    public class UserImplementaion : IUserDetailsRepository
    {
        public async Task<List<User>> GetUserDetails(IAuthenticationRepository iAuthenticationRepository,string token)
        {
            
            GraphServiceClient graphClient = new  GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            }));

            var users = graphClient.Users
                .Request()
                .GetAsync();
            //I have only one user as a result I have not requested next page.
            return users.Result.ToList();
        }
    }
}
