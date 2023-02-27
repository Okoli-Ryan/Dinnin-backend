namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/side-item")]
    public class SideItemController : ControllerBase {

        readonly SideItemService sideItemService;
        readonly IMapper mapper;

        public SideItemController(SideItemService sideItemService, IMapper mapper) {

            this.sideItemService = sideItemService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSideItemByID(Guid ID) {

            var addedSideItem = await sideItemService.GetByID(ID);

            if (addedSideItem is null) return Ok(new DefaultErrorResponse<SideItem>());


            return Ok(new DefaultSuccessResponse<SideItemDto>(addedSideItem));
        }




        [HttpPost()]
        public async Task<IActionResult> AddSideItem([FromBody] SideItemDto sideItemDto) {

            DefaultResponse<SideItemDto> response = new();

            var mappedSideItem = mapper.Map<SideItem>(sideItemDto);

            var addedSideItem = await sideItemService.Save(mappedSideItem);

            if (addedSideItem is null) return Ok(new DefaultErrorResponse<SideItemDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedSideItem;

            return Ok(response);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateSideItem([FromBody] SideItemDto sideItemDto) {


            var mappedSideItem = mapper.Map<SideItem>(sideItemDto);

            var updatedSideItem = await sideItemService.Update(mappedSideItem);

            if (updatedSideItem is null) return Ok(new DefaultErrorResponse<SideItemDto>());


            return Ok(new DefaultSuccessResponse<SideItemDto>(updatedSideItem));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteSideItem(Guid ID) {

            var isDeletedSideItem = await sideItemService.Delete(ID);

            if (!isDeletedSideItem) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedSideItem));
        }





    }
}
