using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// An implementation of <see cref="IApiKeyProvider"/>.
    /// </summary>
    public class ApiKeyProvider : IApiKeyProvider
    {
        /// <summary>
        /// The HTTP header where the ApiKey is expected to be on the request.
        /// </summary>
        public string HeaderName { get; set; }

        /// <summary>
        /// The linkages which contain configuration keys
        /// </summary>
        public IReadOnlyCollection<IApiKeyRoleLink> ApiKeyRoleLinks { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="SingleApiKeyProvider"/>.
        /// </summary>
        /// <param name="apiKeyConfigurationKey">The configuration/secret key which points to the ApiKey value.</param>
        /// <param name="headerName">The name of the header where the ApiKey is found on the request.</param>
        /// <returns></returns>
        public static ApiKeyProvider Initialize(string apiKeyConfigurationKey = ApiKeyConstants.DefaultApiKeyConfigurationKey, string headerName = ApiKeyConstants.DefaultHeaderName)
        {
            headerName = string.IsNullOrWhiteSpace(headerName) ? ApiKeyConstants.DefaultHeaderName : headerName;

            var ret = new ApiKeyProvider();
            ret.HeaderName = headerName;
            ret.ApiKeyRoleLinks = new List<ApiKeyRoleLink>()
            {
                new ApiKeyRoleLink()
                {
                    ConfigurationKey = apiKeyConfigurationKey,
                    Id = Guid.Empty,
                    Name = "default"
                }
            };

            return ret;
        }
    }
}
