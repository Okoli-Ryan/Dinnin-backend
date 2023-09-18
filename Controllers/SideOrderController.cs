namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/side-order")]
    public class SidesOrderController : ControllerBase {

        readonly SidesOrderService sidesOrderService;
        readonly IMapper mapper;

        public SidesOrderController(SidesOrderService sidesOrderService, IMapper mapper) {

            this.sidesOrderService = sidesOrderService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSidesOrderByID(Guid ID) {

            var addedSidesOrder = await sidesOrderService.GetByID(ID);

            if (addedSidesOrder is null) return Ok(new DefaultErrorResponse<SidesOrder>());


            return Ok(new DefaultSuccessResponse<SidesOrderDto>(addedSidesOrder));
        }




        [HttpPost()]
        public async Task<IActionResult> AddSidesOrder([FromBody] SidesOrderDto sidesOrderDto) {

            DefaultResponse<SidesOrderDto> response = new();

            var mappedSidesOrder = mapper.Map<SidesOrder>(sidesOrderDto);

            var addedSidesOrder = await sidesOrderService.Save(mappedSidesOrder);

            if (addedSidesOrder is null) return Ok(new DefaultErrorResponse<SidesOrderDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedSidesOrder;

            return Ok(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateSidesOrder([FromBody] SidesOrderDto sidesOrderDto) {


            var mappedSidesOrder = mapper.Map<SidesOrder>(sidesOrderDto);

            var updatedSidesOrder = await sidesOrderService.Update(mappedSidesOrder);

            if (updatedSidesOrder is null) return Ok(new DefaultErrorResponse<SidesOrderDto>());


            return Ok(new DefaultSuccessResponse<SidesOrderDto>(updatedSidesOrder));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteSidesOrder(Guid ID) {

            var isDeletedSidesOrder = await sidesOrderService.Delete(ID);

            if (!isDeletedSidesOrder) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedSidesOrder));
        }





    }
}
