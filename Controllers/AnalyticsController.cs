namespace OrderUp_API.Controllers {
    [Route("api/v1/[controller]")]
    [ServiceFilter(typeof(ModelValidationActionFilter))]
    [ApiController]
    public class AnalyticsController : ControllerBase {

        readonly ControllerResponseHandler responseHandler;
        readonly AnalyticsService analyticsService;

        public AnalyticsController(AnalyticsService analyticsService) {

            this.analyticsService = analyticsService;
            responseHandler = new ControllerResponseHandler();
        }




        [HttpGet]
        public async Task<IActionResult> GetAnalyticsBreakdown([FromQuery] DateTime? StartTime, [FromQuery] DateTime? EndTime) {

            var response = await analyticsService.GetAnalyticsData(StartTime, EndTime);

            return responseHandler.HandleResponse(response);
        }



        [HttpGet("order-amount")]
        public async Task<IActionResult> GetOrderAmountAnalytics([FromQuery] DateTime? StartTime, [FromQuery] DateTime? EndTime, [FromQuery] string GroupBy) {

            var response = await analyticsService.GetOrderAmountAnalytics(StartTime, EndTime, GroupBy);

            return responseHandler.HandleResponse(response);

        }




        [HttpGet("order-count")]
        public async Task<IActionResult> GetOrderCountAnalytics([FromQuery] DateTime? StartTime, [FromQuery] DateTime? EndTime, [FromQuery] string GroupBy) {

            var response = await analyticsService.GetOrderCountAnalytics(StartTime, EndTime, GroupBy);

            return responseHandler.HandleResponse(response);

        }



        [HttpGet("order-item-count")]
        public async Task<IActionResult> GetOrderItemCountAnalytics([FromQuery] DateTime? StartTime, [FromQuery] DateTime? EndTime) {

            var response = await analyticsService.GetOrderItemCountAnalytics(StartTime, EndTime);

            return responseHandler.HandleResponse(response);

        }
    }
}
