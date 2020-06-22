using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MicrosoftGraphApiDaemonApp.ClientSecretRepository
{
    public interface IClientSecretRepository
    {
        string GetClientSecret();
    }
}
