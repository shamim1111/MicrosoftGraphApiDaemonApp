using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp.GraphApiRepository.TokenRepository
{
    public interface ITokenProcessorRepository
    {
        Task<string> GetGraphApiAccessToken(IAuthenticationRepository iAuthenticationRepository, string clientSecret);

    }
}
