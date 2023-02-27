using System.Text.Json;

namespace OrderUp_API.Middlewares {
    public class ExceptionHandlerMiddleware {

        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex) {

            context.Response.ContentType = "application/json";

            var errorResponse = new DefaultErrorResponse<object>();

            Debug.WriteLine(ex.StackTrace);

            var result = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(result);
        }
    }
}
