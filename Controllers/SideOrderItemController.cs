namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/side-order-item")]
    public class SideOrderItemController : ControllerBase {

        readonly SideOrderItemService sideOrderItemService;
        readonly IMapper mapper;

        public SideOrderItemController(SideOrderItemService sideOrderItemService, IMapper mapper) {

            this.sideOrderItemService = sideOrderItemService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSideOrderItemByID(Guid ID) {

            var addedSideOrderItem = await sideOrderItemService.GetByID(ID);

            if (addedSideOrderItem is null) return Ok(new DefaultErrorResponse<SideOrderItem>());


            return Ok(new DefaultSuccessResponse<SideOrderItemDto>(addedSideOrderItem));
        }




        [HttpPost()]
        public async Task<IActionResult> AddSideOrderItem([FromBody] SideOrderItemDto sideOrderItemDto) {

            DefaultResponse<SideOrderItemDto> response = new();

            var mappedSideOrderItem = mapper.Map<SideOrderItem>(sideOrderItemDto);

            var addedSideOrderItem = await sideOrderItemService.Save(mappedSideOrderItem);

            if (addedSideOrderItem is null) return Ok(new DefaultErrorResponse<SideOrderItemDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedSideOrderItem;

            return Ok(response);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateSideOrderItem([FromBody] SideOrderItemDto sideOrderItemDto) {


            var mappedSideOrderItem = mapper.Map<SideOrderItem>(sideOrderItemDto);

            var updatedSideOrderItem = await sideOrderItemService.Update(mappedSideOrderItem);

            if (updatedSideOrderItem is null) return Ok(new DefaultErrorResponse<SideOrderItemDto>());


            return Ok(new DefaultSuccessResponse<SideOrderItemDto>(updatedSideOrderItem));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteSideOrderItem(Guid ID) {

            var isDeletedSideOrderItem = await sideOrderItemService.Delete(ID);

            if (!isDeletedSideOrderItem) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedSideOrderItem));
        }





    }
}
