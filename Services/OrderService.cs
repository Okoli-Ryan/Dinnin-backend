using OrderUp_API.MessageConsumers;
using System.Net;

namespace OrderUp_API.Services {
    public class OrderService {

        private readonly OrderRepository orderRepository;
        private readonly OrderItemRepository orderItemRepository;
        private readonly MenuItemRepository menuItemRepository;
        private readonly TableRepository tableRepository;
        private readonly IMapper mapper;
        private readonly HttpContext httpContext;
        private readonly OrderUpDbContext _dbContext;
        private readonly PusherService pusherService;
        private readonly MessageProducerService messageProducerService;

        public OrderService(OrderRepository orderRepository, IMapper mapper, MenuItemRepository menuItemRepository, OrderItemRepository orderItemRepository, IHttpContextAccessor httpContextAccessor, OrderUpDbContext dbContext, PusherService pusherService, TableRepository tableRepository, MessageProducerService messageProducerService) {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.menuItemRepository = menuItemRepository;
            this.orderItemRepository = orderItemRepository;
            this.pusherService = pusherService;
            this.tableRepository = tableRepository;
            this.messageProducerService = messageProducerService;
            httpContext = httpContextAccessor.HttpContext;
            _dbContext = dbContext;
        }

        public async Task<DefaultResponse<MakeOrder>> SaveOrder(MakeOrder OrderRequest) {

            //Convert to Model before operations
            var OrderItems = mapper.Map<List<OrderItem>>(OrderRequest.OrderItems);
            var Order = mapper.Map<Order>(OrderRequest.Order);

            var menuItems = await menuItemRepository.GetMenuItemsByRestaurantID(Order.RestaurantId);


            if (menuItems is null) return new DefaultErrorResponse<MakeOrder>() {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseData = null,
                ResponseMessage = "Unable to find Restaurant"
            };



            foreach (var item in OrderItems) {

                var menuItem = menuItems.Find(x => x.ID.Equals(item.MenuItemID));

                Order.OrderAmount += menuItem.Price * item.Quantity;

                item.MenuItemName = menuItem.MenuItemName;

                item.OrderItemPrice = menuItem.Price;

            }

            Order.OrderStatus = OrderModelConstants.INITIAL;



            using var transaction = _dbContext.Database.BeginTransaction();

            var SavedOrder = await orderRepository.Save(Order);

            if (SavedOrder is null) return new DefaultErrorResponse<MakeOrder>();





            foreach (var item in OrderItems) {

                item.OrderID = SavedOrder.ID;

            }


            var SavedOrderItems = await orderItemRepository.Save(OrderItems);

            if (SavedOrderItems is null) return new DefaultErrorResponse<MakeOrder>();


            transaction.Commit();




            var UserTable = await tableRepository.GetByID(SavedOrder.TableID);

            if (UserTable is null) return new DefaultErrorResponse<MakeOrder>();



            //Send Response To Dashboard Client
            SavedOrder.OrderItems = SavedOrderItems;
            SavedOrder.Table = UserTable;

            var RestaurantIdString = GuidStringConverter.GuidToString(SavedOrder.RestaurantId);

            var MappedOrder = mapper.Map<OrderDto>(SavedOrder);
            var MappedOrderItems = mapper.Map<List<OrderItemDto>>(SavedOrderItems);

            var pusherResponse = await pusherService.TriggerMessage(MappedOrder, OrderModelConstants.NEW_ORDER_EVENT, RestaurantIdString);

            messageProducerService.SendMessage(MessageQueueTopics.PUSH_NOTIFICATION, new PushNotificationBody () {

                RestaurantID = SavedOrder.RestaurantId,
                Message = new PushNotificationMessage {
                    Body = "A New Order has arrived",
                    Title = "New Order"
                }
            });


            MakeOrder OrderResponse = new() {
                Order = MappedOrder,
                OrderItems = MappedOrderItems
            };


            var mappedRseponse = mapper.Map<MakeOrder>(OrderResponse);

            return new DefaultSuccessResponse<MakeOrder>(mappedRseponse);
        }

        public async Task<List<OrderDto>> Save(List<Order> order) {

            var addedOrder = await orderRepository.Save(order);

            return mapper.Map<List<OrderDto>>(addedOrder);
        }


        public async Task<DefaultResponse<T>> GetActiveOrders<T>(DateTime LastTime) where T : List<OrderDto> {

            string RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

            if (!Guid.TryParse(RestaurantIDString, out Guid RestaurantId)) {

                return new DefaultResponse<T> {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null,
                    ResponseMessage = "Unauthorized Access"
                };
            }

            var OrderList = await orderRepository.GetActiveOrders(RestaurantId, LastTime);

            if (OrderList is null) {
                return new DefaultErrorResponse<T>();
            }

            var ListOfOrders = mapper.Map<T>(OrderList);
            return new DefaultSuccessResponse<T>(ListOfOrders);
        }


        public async Task<DefaultResponse<T>> GetOrdersByRestaurantID<T>(int Page) where T : List<OrderDto> {

            string RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

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
