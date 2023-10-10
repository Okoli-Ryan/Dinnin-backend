using System.Net;

namespace OrderUp_API.Services {
    public class OrderService {

        private readonly OrderRepository orderRepository;
        private readonly OrderItemRepository orderItemRepository;
        private readonly MenuItemRepository menuItemRepository;
        private readonly TableRepository tableRepository;
        private readonly IMapper mapper;
        private readonly HttpContext _context;
        private readonly OrderUpDbContext _dbContext;
        private readonly PusherService pusherService;

        public OrderService(OrderRepository orderRepository, IMapper mapper, MenuItemRepository menuItemRepository, OrderItemRepository orderItemRepository, IHttpContextAccessor httpContextAccessor, OrderUpDbContext dbContext, PusherService pusherService, TableRepository tableRepository) {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.menuItemRepository = menuItemRepository;
            this.orderItemRepository = orderItemRepository;
            this.pusherService = pusherService;
            this.tableRepository = tableRepository; 
            _context = httpContextAccessor.HttpContext;
            _dbContext = dbContext;
        }

        public async Task<DefaultResponse<MakeOrder>> SaveOrder(MakeOrder OrderRequest) {

            decimal Price = 0;

            //Convert to Model before operations
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




            var UserTable = await tableRepository.GetByID(Order.tableId);

            if (UserTable is null) return new DefaultErrorResponse<MakeOrder>();



            //Send Response To Dashboard Client
            SavedOrder.OrderItems = SavedOrderItems;
            SavedOrder.Table = UserTable;

            var RestaurantIdString = GuidToString.Convert(SavedOrder.RestaurantId);

            var pusherResponse = await pusherService.TriggerMessage(SavedOrder, OrderModelConstants.NEW_ORDER_EVENT, RestaurantIdString);


            MakeOrder OrderResponse = new() {
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

            string RestaurantIDString = GetJwtValue.GetValueFromBearerToken(_context, RestaurantIdentifier.RestaurantClaimType);

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
