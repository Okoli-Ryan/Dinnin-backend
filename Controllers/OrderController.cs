namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/order")]
    public class OrderController : ControllerBase {

        readonly OrderService orderService;
        readonly IMapper mapper;

        public OrderController(OrderService orderService, IMapper mapper) {

            this.orderService = orderService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetOrderByID(Guid ID) {

            var order = await orderService.GetByID(ID);

            if (order is null) return BadRequest(order);


            return Ok(order);
        }


        [HttpGet("restaurant/{page}")]
        public async Task<IActionResult> GetOrdersByRestaurantID(int Page) {

            var orders = await orderService.GetOrdersByRestaurantID<List<OrderDto>>(Page);

            if (orders.ResponseCode.Equals(ResponseCodes.UNAUTHORIZED)) return Unauthorized(orders);
            if (orders.ResponseCode.Equals(ResponseCodes.FAILURE)) return BadRequest(orders);


            return Ok(orders);
        }




        [HttpPost()]
        public async Task<IActionResult> AddOrder([FromBody] MakeOrder Order) {

            //DefaultResponse<OrderDto> response = new();

            var addedOrder = await orderService.SaveOrder(Order);

            if (addedOrder is null) return BadRequest(addedOrder);


            return Ok(addedOrder);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto) {


            var mappedOrder = mapper.Map<Order>(orderDto);

            var updatedOrder = await orderService.Update(mappedOrder);

            if (updatedOrder is null) return Ok(new DefaultErrorResponse<OrderDto>());


            return Ok(new DefaultSuccessResponse<OrderDto>(updatedOrder));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteOrder(Guid ID) {

            var isDeletedOrder = await orderService.Delete(ID);

            if (!isDeletedOrder) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedOrder));
        }





    }
}
