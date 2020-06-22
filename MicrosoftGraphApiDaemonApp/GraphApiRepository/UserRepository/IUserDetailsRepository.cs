using Microsoft.Graph;
using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp.GraphApiRepository.UserRepository
{
    public interface IUserDetailsRepository
    {
        Task<List<User>> GetUserDetails(IAuthenticationRepository iAuthenticationRepository, string token);
    }
}
