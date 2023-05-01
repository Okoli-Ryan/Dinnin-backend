namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/restaurant")]
    public class RestaurantController : ControllerBase {

        readonly RestaurantService restaurantService;
        readonly IMapper mapper;
        readonly ControllerResponseHandler ResponseHandler;

        public RestaurantController(RestaurantService restaurantService, IMapper mapper) {

            this.restaurantService = restaurantService;
            this.mapper = mapper;
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




        [HttpPost()]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantDto restaurantDto) {

            DefaultResponse<RestaurantDto> response = new();

            var mappedRestaurant = mapper.Map<Restaurant>(restaurantDto);

            var addedRestaurant = await restaurantService.Save(mappedRestaurant);

            if (addedRestaurant is null) return Ok(new DefaultErrorResponse<RestaurantDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedRestaurant;

            return Ok(response);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantDto restaurantDto) {


            var mappedRestaurant = mapper.Map<Restaurant>(restaurantDto);

            var updatedRestaurant = await restaurantService.Update(mappedRestaurant);

            if (updatedRestaurant is null) return Ok(new DefaultErrorResponse<RestaurantDto>());


            return Ok(new DefaultSuccessResponse<RestaurantDto>(updatedRestaurant));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteRestaurant(Guid ID) {

            var isDeletedRestaurant = await restaurantService.Delete(ID);

            if (!isDeletedRestaurant) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedRestaurant));
        }





    }
}
