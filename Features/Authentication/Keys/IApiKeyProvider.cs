using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// Defines a provider for handling ApiKey authentication. 
    /// </summary>
    public interface IApiKeyProvider
    {
        string HeaderName { get; }

        IReadOnlyCollection<IApiKeyRoleLink> ApiKeyRoleLinks { get; }
    }
}
