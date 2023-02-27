namespace OrderUp_API.Services {
    public class SideItemService {

        readonly IMapper mapper;
        readonly SideItemRepository sideItemRepository;

        public SideItemService(IMapper mapper, SideItemRepository sideItemRepository) {
            this.mapper = mapper;
            this.sideItemRepository = sideItemRepository;
        }

        public async Task<SideItemDto> Save(SideItem sideItem) {

            var addedSideItem = await sideItemRepository.Save(sideItem);
            return mapper.Map<SideItemDto>(addedSideItem);
        }

        public async Task<List<SideItemDto>> Save(List<SideItem> sideItem) {

            var addedSideItem = await sideItemRepository.Save(sideItem);
            return mapper.Map<List<SideItemDto>>(addedSideItem);
        }

        public async Task<SideItemDto> GetByID(Guid ID) {

            var sideItem = await sideItemRepository.GetByID(ID);

            return mapper.Map<SideItemDto>(sideItem);
        }

        public async Task<SideItemDto> Update(SideItem sideItem) {

            var updatedSideItem = await sideItemRepository.Update(sideItem);

            return mapper.Map<SideItemDto>(updatedSideItem);
        }

        public async Task<bool> Delete(Guid ID) {

            return await sideItemRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<SideItem> sideItem) {

            return await sideItemRepository.Delete(sideItem);
        }
    }
}
