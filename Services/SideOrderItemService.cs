namespace OrderUp_API.Services {
    public class SideOrderItemService {

        private readonly SideOrderItemRepository SideOrderItemRepository;
        private readonly IMapper mapper;

        public SideOrderItemService(SideOrderItemRepository SideOrderItemRepository, IMapper mapper) {
            this.SideOrderItemRepository = SideOrderItemRepository;
            this.mapper = mapper;
        }

        public async Task<SideOrderItemDto> Save(SideOrderItem SideOrderItem) {

            var addedSideOrderItem = await SideOrderItemRepository.Save(SideOrderItem);

            return mapper.Map<SideOrderItemDto>(addedSideOrderItem);
        }

        public async Task<List<SideOrderItemDto>> Save(List<SideOrderItem> SideOrderItem) {

            var addedSideOrderItem = await SideOrderItemRepository.Save(SideOrderItem);

            return mapper.Map<List<SideOrderItemDto>>(addedSideOrderItem);
        }

        public async Task<SideOrderItemDto> GetByID(Guid ID) {

            var SideOrderItem = await SideOrderItemRepository.GetByID(ID);

            return mapper.Map<SideOrderItemDto>(SideOrderItem);
        }

        public async Task<SideOrderItemDto> Update(SideOrderItem SideOrderItem) {

            var updatedSideOrderItem = await SideOrderItemRepository.Update(SideOrderItem);

            return mapper.Map<SideOrderItemDto>(updatedSideOrderItem);
        }

        public async Task<bool> Delete(Guid ID) {

            return await SideOrderItemRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<SideOrderItem> SideOrderItem) {

            return await SideOrderItemRepository.Delete(SideOrderItem);
        }

    }
}
