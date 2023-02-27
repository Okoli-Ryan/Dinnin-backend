namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/saved-restaurant")]
    public class SavedRestaurantController : ControllerBase {

        readonly SavedRestaurantService savedRestaurantService;
        readonly IMapper mapper;

        public SavedRestaurantController(SavedRestaurantService savedRestaurantService, IMapper mapper) {

            this.savedRestaurantService = savedRestaurantService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSavedRestaurantByID(Guid ID) {

            var addedSavedRestaurant = await savedRestaurantService.GetByID(ID);

            if (addedSavedRestaurant is null) return Ok(new DefaultErrorResponse<SavedRestaurant>());


            return Ok(new DefaultSuccessResponse<SavedRestaurantDto>(addedSavedRestaurant));
        }




        [HttpPost()]
        public async Task<IActionResult> AddSavedRestaurant([FromBody] SavedRestaurantDto savedRestaurantDto) {

            DefaultResponse<SavedRestaurantDto> response = new();

            var mappedSavedRestaurant = mapper.Map<SavedRestaurant>(savedRestaurantDto);

            var addedSavedRestaurant = await savedRestaurantService.Save(mappedSavedRestaurant);

            if (addedSavedRestaurant is null) return Ok(new DefaultErrorResponse<SavedRestaurantDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedSavedRestaurant;

            return Ok(response);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateSavedRestaurant([FromBody] SavedRestaurantDto savedRestaurantDto) {


            var mappedSavedRestaurant = mapper.Map<SavedRestaurant>(savedRestaurantDto);

            var updatedSavedRestaurant = await savedRestaurantService.Update(mappedSavedRestaurant);

            if (updatedSavedRestaurant is null) return Ok(new DefaultErrorResponse<SavedRestaurantDto>());


            return Ok(new DefaultSuccessResponse<SavedRestaurantDto>(updatedSavedRestaurant));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteSavedRestaurant(Guid ID) {

            var isDeletedSavedRestaurant = await savedRestaurantService.Delete(ID);

            if (!isDeletedSavedRestaurant) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedSavedRestaurant));
        }





    }
}
