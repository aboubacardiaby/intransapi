using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// Defines a link between a role and an ApiKey value.
    /// </summary>
    public interface IApiKeyRoleLink
    {
        /// <summary>
        /// A unique identifer for the role.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// A Frendly name for the role.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The configuration key containing the underlying ApiKey value.
        /// </summary>
        string ConfigurationKey { get; }
    }
}
