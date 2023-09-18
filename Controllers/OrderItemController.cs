namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/order-item")]
    public class OrderItemController : ControllerBase {

        readonly OrderItemService orderItemService;
        readonly IMapper mapper;

        public OrderItemController(OrderItemService orderItemService, IMapper mapper) {

            this.orderItemService = orderItemService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetOrderItemByID(Guid ID) {

            var addedOrderItem = await orderItemService.GetByID(ID);

            if (addedOrderItem is null) return Ok(new DefaultErrorResponse<OrderItem>());


            return Ok(new DefaultSuccessResponse<OrderItemDto>(addedOrderItem));
        }




        [HttpPost()]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemDto orderItemDto) {

            DefaultResponse<OrderItemDto> response = new();

            var mappedOrderItem = mapper.Map<OrderItem>(orderItemDto);

            var addedOrderItem = await orderItemService.Save(mappedOrderItem);

            if (addedOrderItem is null) return Ok(new DefaultErrorResponse<OrderItemDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedOrderItem;

            return Ok(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemDto orderItemDto) {


            var mappedOrderItem = mapper.Map<OrderItem>(orderItemDto);

            var updatedOrderItem = await orderItemService.Update(mappedOrderItem);

            if (updatedOrderItem is null) return Ok(new DefaultErrorResponse<OrderItemDto>());


            return Ok(new DefaultSuccessResponse<OrderItemDto>(updatedOrderItem));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteOrderItem(Guid ID) {

            var isDeletedOrderItem = await orderItemService.Delete(ID);

            if (!isDeletedOrderItem) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedOrderItem));
        }





    }
}
