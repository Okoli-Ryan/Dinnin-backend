﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderUp_API.Classes.AnalyticsModels;

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


        [HttpGet("order-amount")]
        public async Task<IActionResult> GetOrderAmountAnalytics([FromQuery] DateTime? StartTime, [FromQuery] DateTime? EndTime) {

            var response = await analyticsService.GetOrderAmountAnalytics(StartTime, EndTime);

            return responseHandler.HandleResponse(response);

        }

        [HttpGet("order-count")]
        public async Task<IActionResult> GetOrderCountAnalytics([FromQuery] DateTime? StartTime, [FromQuery] DateTime? EndTime) {

            var response = await analyticsService.GetOrderCountAnalytics(StartTime, EndTime);

            return responseHandler.HandleResponse(response);

        }
    }
}
