using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// Contains extensions for Swagger configuration.
    /// </summary>
    public static class SwaggerExtentions
    {
        /// <summary>
        /// Configures <see cref="SwaggerGenOptions"/> with details for ApiKey authentication.
        /// </summary>
        /// <param name="source"><see cref="SwaggerGenOptions"/></param>
        /// <param name="apiKeyProvider">An instance of the <see cref="IApiKeyProvider"/> being used.</param>
        public static void WithApiKeySwagger(this SwaggerGenOptions source, IApiKeyProvider apiKeyProvider)
        {
            string headerName = apiKeyProvider == null || string.IsNullOrWhiteSpace(apiKeyProvider.HeaderName) ? ApiKeyConstants.DefaultHeaderName : apiKeyProvider.HeaderName;

            source.WithApiKeySwagger(headerName);
        }

        /// <summary>
        /// Configures <see cref="SwaggerGenOptions"/> with ApiKey details.
        /// </summary>
        /// <param name="source"><see cref="SwaggerGenOptions"/></param>
        /// <param name="headerName">The HTTP header name the ApiKey is expected to be in.</param>
        public static void WithApiKeySwagger(this SwaggerGenOptions source, string headerName)
        {
            if (source == null)
            {
                return;
            }

            headerName = string.IsNullOrWhiteSpace(headerName) ? ApiKeyConstants.DefaultHeaderName : headerName;

            source.AddSecurityDefinition(headerName, new OpenApiSecurityScheme
            {
                Description = $"An ApiKey must be provided to access the endpoints. {headerName}: yourApiKeyValue",
                In = ParameterLocation.Header,
                Name = headerName,
                Type = SecuritySchemeType.ApiKey,
            });

            source.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = headerName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = headerName
                            },
                        },
                        new string[] {}
                    }
                });
        }
    }
}
