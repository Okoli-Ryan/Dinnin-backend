﻿using Microsoft.AspNetCore.Authorization;
using OrderUp_API.Attributes;

namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/restaurant")]
    public class RestaurantController : ControllerBase {

        readonly RestaurantService restaurantService;
        readonly ControllerResponseHandler ResponseHandler;

        public RestaurantController(RestaurantService restaurantService) {

            this.restaurantService = restaurantService;
            ResponseHandler = new ControllerResponseHandler();
        }


        [HttpGet("s/{Slug}")]
        public async Task<IActionResult> GetRestaurantBySlug(string Slug) {

            var RestaurantResponse = await restaurantService.GetRestaurantBySlug(Slug);

            if (RestaurantResponse is null) return NotFound(RestaurantResponse);

            return Ok(RestaurantResponse);
        }

        [HttpGet("slug/{Slug}")]
        public async Task<IActionResult> DoesSlugExist(string Slug) {

            var Response = await restaurantService.DoesSlugExist(Slug);


            return Ok(Response);
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetRestaurantByID(Guid ID) {

            var addedRestaurant = await restaurantService.GetByID(ID);

            if (addedRestaurant is null) return Ok(new DefaultErrorResponse<Restaurant>());


            return Ok(new DefaultSuccessResponse<RestaurantDto>(addedRestaurant));
        }



        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantDto restaurantDto) {

            var response = await restaurantService.Save(restaurantDto);

            return ResponseHandler.HandleResponse(response);
        }


        [PermissionRequired(PermissionName.RESTAURANT__UPDATE_RESTAURANT)]
        [HttpPut()]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantDto restaurantDto) {

            var response = await restaurantService.Update(restaurantDto);

            return ResponseHandler.HandleResponse(response);

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteRestaurant(Guid ID) {

            var isDeletedRestaurant = await restaurantService.Delete(ID);

            if (!isDeletedRestaurant) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedRestaurant));
        }





    }
}
