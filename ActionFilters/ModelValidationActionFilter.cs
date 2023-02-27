using Microsoft.AspNetCore.Mvc.Filters;

namespace OrderUp_API.ActionFilters {
    public class ModelValidationActionFilter : IActionFilter {

        public void OnActionExecuted(ActionExecutedContext context) {
            
        }

        public void OnActionExecuting(ActionExecutingContext context) {

            if (!context.ModelState.IsValid) {

                var Result = new DefaultErrorResponse<object>() {
                    ResponseCode = ResponseCodes.INSUFFICIENT_DATA,
                    ResponseMessage = ResponseMessages.INSUFFICIENT_DATA,
                    ResponseData = null
                };


                context.Result = new BadRequestObjectResult(Result);
            }
        }
    }
}
