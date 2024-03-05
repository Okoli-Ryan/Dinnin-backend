using Microsoft.AspNetCore.Mvc.Filters;

namespace OrderUp_API.ActionFilters {
    public class AuthorizationActionFilter : IActionFilter {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context) {

        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context) {

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment.Equals("Development")) return;

            if (context.HttpContext.Request.Headers.ContainsKey("x_api_key")) {

                string apiKey = context.HttpContext.Request.Headers["x_api_key"];

                string STORED_API_KEY = ConfigurationUtil.GetConfigurationValue("x_api_key");

                if (!apiKey.Equals(STORED_API_KEY)) {
                    context.Result = new UnauthorizedResult();
                }
                return;
            }

            context.Result = new UnauthorizedResult();

            return;
        }
    }
}
