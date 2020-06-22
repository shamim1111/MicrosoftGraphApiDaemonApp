using MicrosoftGraphApiDaemonApp.AuthenticationRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphApiDaemonApp.KeyVaultAccessRepository
{
    public interface IKeyVaultAccessRepository
    {
        Task<string> GetClientSecretFromKeyVault(IAuthenticationRepository iAuthRepository);
    }
}
