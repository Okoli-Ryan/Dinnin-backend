using Microsoft.AspNetCore.Mvc.Filters;
using OrderUp_API.Attributes;
using System.Reflection;

namespace OrderUp_API.ActionFilters {
    public class PermissionActionFilter : IActionFilter {

        public void OnActionExecuted(ActionExecutedContext context) {
            
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            var controller = context.Controller as ControllerBase;

            if(controller != null) {

                var actionMethod = controller.ControllerContext.ActionDescriptor.MethodInfo;
                var permissionAttribute = actionMethod.GetCustomAttributes<PermissionRequiredAttribute>().FirstOrDefault();

                if (permissionAttribute != null) {

                    PermissionName requiredPermission = permissionAttribute.PermissionName;

                    var isUserAuthorized = context.HttpContext
                                                 .User
                                                 .HasClaim(claim => claim.Type == ClaimType.PERMISSION_CLAIM_TYPE && claim.Value.Contains((Char)requiredPermission));

                    if (!isUserAuthorized) {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
