namespace OrderUp_API.Services {
    public class OrderItemService {

        private readonly OrderItemRepository orderItemRepository;
        private readonly IMapper mapper;

        public OrderItemService(OrderItemRepository orderItemRepository, IMapper mapper) {
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
        }

        public async Task<OrderItemDto> Save(OrderItem orderItem) {

            var addedOrderItem = await orderItemRepository.Save(orderItem);

            return mapper.Map<OrderItemDto>(addedOrderItem);
        }

        public async Task<List<OrderItemDto>> Save(List<OrderItem> orderItem) {

            var addedOrderItem = await orderItemRepository.Save(orderItem);

            return mapper.Map<List<OrderItemDto>>(addedOrderItem);
        }

        public async Task<OrderItemDto> GetByID(Guid ID) {

            var orderItem = await orderItemRepository.GetByID(ID);

            return mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task<OrderItemDto> Update(OrderItem orderItem) {

            var updatedOrderItem = await orderItemRepository.Update(orderItem);

            return mapper.Map<OrderItemDto>(updatedOrderItem);
        }

        public async Task<bool> Delete(Guid ID) {

            return await orderItemRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<OrderItem> orderItem) {

            return await orderItemRepository.Delete(orderItem);
        }

    }
}
