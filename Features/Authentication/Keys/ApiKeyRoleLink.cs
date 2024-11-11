using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// A default implementation of <see cref="IApiKeyRoleLink"/>.
    /// </summary>
    public class ApiKeyRoleLink : IApiKeyRoleLink
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ConfigurationKey { get; set; }
    }
}
