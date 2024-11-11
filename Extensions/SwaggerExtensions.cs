using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialTransactionApi.Extensions
{
    public static class SwaggerExtensions
    {
        /// Configures <see cref="SwaggerGenOptions"/> with a common template for an organizational API.
        /// </summary>
        /// <param name="source">source</param>
        public static void WithCommonSwagger(this SwaggerGenOptions source,
            string apiName = "UNNAMED", string apiVersion = "v1",
            string description = "",
            string environmentName = "NOTSET", string buildNumber = "NOTSET", string helpUri = "https://dev.azure.com/owens-minor-commercial")
        {
            if (source == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(apiVersion) || !apiVersion.StartsWith("v"))
            {
                apiVersion = "v1";
            }

            if (string.IsNullOrWhiteSpace(apiName))
            {
                apiName = "UNNAMED";
            }

            source.UseInlineDefinitionsForEnums();

            source.SwaggerDoc(apiVersion, new OpenApiInfo
            {
                Title = $"{apiName} API (build: {buildNumber}, environment: {environmentName})",
                Description = description ?? string.Empty,
                Version = apiVersion,
                Contact = new OpenApiContact()
                {
                    Name = "noreply@owens-minor.com",
                    Email = "noreply@owens-minor.com",
                    Url = helpUri.AsUri(),
                },
                License = new OpenApiLicense
                {
                    Name = "License Undefined",
                    Url = helpUri.AsUri(),
                }
            });
        }

        private const string _defaultUri = "https://dev.azure.com/owens-minor-commercial";

        /// <summary>
        /// AsUri
        /// </summary>
        /// <param name="uriString"></param>
        /// <returns></returns>
        private static Uri AsUri(this string uriString)
        {
            if (string.IsNullOrWhiteSpace(uriString))
            {
                return new Uri(_defaultUri);
            }

            try
            {
                return new Uri(uriString);
            }
            catch (UriFormatException)
            {
                return new Uri(_defaultUri);
            }
        }
    }
}
  