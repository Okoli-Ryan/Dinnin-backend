namespace OrderUp_API.Services {
    public class OrderService {

        private readonly OrderRepository orderRepository;
        private readonly OrderItemRepository orderItemRepository;
        private readonly MenuItemRepository menuItemRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpContext _context;
        private readonly OrderUpDbContext _dbContext;

        public OrderService(OrderRepository orderRepository, IMapper mapper, MenuItemRepository menuItemRepository, OrderItemRepository orderItemRepository, IHttpContextAccessor httpContextAccessor, OrderUpDbContext dbContext) {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.menuItemRepository = menuItemRepository;
            this.orderItemRepository = orderItemRepository;
            this.httpContextAccessor = httpContextAccessor;
            _context = httpContextAccessor.HttpContext;
            _dbContext = dbContext;
        }

        public async Task<DefaultResponse<MakeOrder>> SaveOrder(MakeOrder OrderRequest) {

            decimal Price = 0;

            var OrderItems = OrderRequest.OrderItems;
            var Order = OrderRequest.Order;

            var menuItems = await menuItemRepository.GetMenuItemsByRestaurantID(Order.restaurantId);


            if (menuItems is null) return new DefaultErrorResponse<MakeOrder>() {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseData = null,
                ResponseMessage = "Unable to find Restaurant"
            };



            foreach (var item in OrderItems) {

                var menuItem = menuItems.Find(x => x.ID.Equals(item.menuItemId));

                Price += menuItem.Price * item.quantity;

                item.menuItemName = menuItem.MenuItemName;

                item.itemPrice = menuItem.Price;

            }

            Order.price = Price;

            Order.orderStatus = OrderModelConstants.INITIAL;

            Order MappedOrder = mapper.Map<Order>(Order);




            using var transaction = _dbContext.Database.BeginTransaction();

            var SavedOrder = await orderRepository.Save(MappedOrder);

            if (SavedOrder is null) return new DefaultErrorResponse<MakeOrder>();






            foreach (var item in OrderItems) {

                item.orderId = SavedOrder.ID;

            }

            List<OrderItem> MappedOrderItems = mapper.Map<List<OrderItem>>(OrderItems);

            var SavedOrderItems = await orderItemRepository.Save(MappedOrderItems);

            if (SavedOrderItems is null) return new DefaultErrorResponse<MakeOrder>();


            transaction.Commit();



            MakeOrder OrderResponse = new MakeOrder() {
                Order = mapper.Map<OrderDto>(SavedOrder),
                OrderItems = mapper.Map<List<OrderItemDto>>(SavedOrderItems)
            };


            var mappedRseponse = mapper.Map<MakeOrder>(OrderResponse);

            return new DefaultSuccessResponse<MakeOrder>(mappedRseponse);
        }

        public async Task<List<OrderDto>> Save(List<Order> order) {

            var addedOrder = await orderRepository.Save(order);

            return mapper.Map<List<OrderDto>>(addedOrder);
        }


        public async Task<DefaultResponse<T>> GetActiveOrders<T>() where T : List<OrderDto> {

            string RestaurantIDString = GetJwtValue.GetValueFromBearerToken(_context, RestaurantIdentifier.RestaurantClaimType);

            if (!Guid.TryParse(RestaurantIDString, out Guid restaurantId)) {

                return new DefaultResponse<T> {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null,
                    ResponseMessage = "Unauthorized Access"
                };
            }

            var OrderList = await orderRepository.GetActiveOrders(restaurantId);

            if (OrderList is null) {
                return new DefaultErrorResponse<T>();
            }

            var ListOfOrders = mapper.Map<T>(OrderList);
            return new DefaultSuccessResponse<T>(ListOfOrders);
        }


        public async Task<DefaultResponse<T>> GetOrdersByRestaurantID<T>(int Page) where T : List<OrderDto> {

            var context = httpContextAccessor.HttpContext;

            string RestaurantIDString = GetJwtValue.GetValueFromBearerToken(context, RestaurantIdentifier.RestaurantClaimType);

            if (Guid.TryParse(RestaurantIDString, out Guid restaurantId)) {
                var OrderList = await orderRepository.GetOrdersByRestaurantID(restaurantId, Page);

                var ListOfOrders = mapper.Map<T>(OrderList);

                return new DefaultSuccessResponse<T>(ListOfOrders);

            }
            else {
                // handle the case where the string is not a valid GUID...
                return new DefaultResponse<T>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null,
                    ResponseMessage = "Unauthorized Access"
                };
            }

        }




        public async Task<OrderDto> GetByID(Guid ID) {

            var order = await orderRepository.GetByID(ID);

            return mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> Update(Order order) {

            var updatedOrder = await orderRepository.Update(order);

            return mapper.Map<OrderDto>(updatedOrder);
        }

        public async Task<bool> Delete(Guid ID) {

            return await orderRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Order> order) {

            return await orderRepository.Delete(order);
        }

    }
}
