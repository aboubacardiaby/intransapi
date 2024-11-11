using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using FinancialTransactionApi.Features.Authentication.Keys;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// Extensions which provide shortcuts for configuring the <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ApiKey Authentication to the <see cref="IServiceCollection"/> along with the default <see cref="ApiKeyProvider"/> added
        /// as the <see cref="IApiKeyProvider"/>. 
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="apiKeyProvider">An instance of an <see cref="IApiKeyProvider"/> implementation.</param>
        public static void AddApiKeyAuthentication(this IServiceCollection services)
        {
            services.AddApiKeyAuthentication(ApiKeyProvider.Initialize());
        }

        /// <summary>
        /// Adds ApiKey Authentication to the <see cref="IServiceCollection"/> along with a specified instnace of an <see cref="IApiKeyProvider"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="apiKeyProvider">An instance of an <see cref="IApiKeyProvider"/> implementation.</param>
        public static void AddApiKeyAuthentication(this IServiceCollection services, IApiKeyProvider apiKeyProvider)
        {
            if (services == null)
            {
                throw new InvalidOperationException($"The {nameof(IServiceCollection)} is null.");
            }

            if (apiKeyProvider == null)
            {
                throw new InvalidOperationException($"The {nameof(IApiKeyProvider)}, {nameof(apiKeyProvider)}, is null.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            });

            services.AddSingleton<IApiKeyProvider>(apiKeyProvider);
        }
    }
}
