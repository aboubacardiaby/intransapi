using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialTransactionApi.Features.Authentication.Keys
{
    /// <summary>
    /// Defines an attribute for enforcing an API Key.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// OnActionExecutionAsync
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="next">next</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context?.HttpContext?.RequestServices == null)
            {
                throw new InvalidOperationException($"Aspects of the {nameof(ActionExecutingContext)} are null.");
            }

            var apiKeyProvider = context.HttpContext.RequestServices.GetRequiredService<IApiKeyProvider>();

            if (apiKeyProvider == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = $"No {nameof(IApiKeyProvider)} is configured."
                };

                return;
            }

            // Attempt to get the Api Key provided on the request ...
            if (string.IsNullOrWhiteSpace(apiKeyProvider.HeaderName) ||
                !context.HttpContext.Request.Headers.TryGetValue(apiKeyProvider.HeaderName, out var apiKeyFromRequest))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = $"Api Key was not provided [{apiKeyProvider.HeaderName}]"
                };
                return;
            }

            // Check to see if any of the keys match ...
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            bool found = false;

            foreach (var apiKeyRoleLink in apiKeyProvider.ApiKeyRoleLinks)
            {
                if (!string.IsNullOrWhiteSpace(apiKeyRoleLink.ConfigurationKey) &&
                    string.Compare(apiKeyFromRequest, appSettings.GetValue<string>(apiKeyRoleLink.ConfigurationKey), false) == 0)
                {
                    found = true;
                    break;
                }
            }

            // If not found ... reject
            if (!found)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = $"Api Key is not valid."
                };
                return;
            }

            await next();
        }
    }
}
