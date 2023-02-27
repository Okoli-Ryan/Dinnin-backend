namespace OrderUp_API.Services {
    public class MenuItemService {

        readonly IMapper mapper;
        readonly MenuItemRepository menuItemRepository;

        public MenuItemService(IMapper mapper, MenuItemRepository menuItemRepository) {
            this.mapper = mapper;
            this.menuItemRepository = menuItemRepository;
        }

        public async Task<MenuItemDto> Save(MenuItem menuItem) {

            var addedMenuItem = await menuItemRepository.Save(menuItem);
            return mapper.Map<MenuItemDto>(addedMenuItem);
        }

        public async Task<List<MenuItemDto>> Save(List<MenuItem> menuItem) {

            var addedMenuItem = await menuItemRepository.Save(menuItem);
            return mapper.Map<List<MenuItemDto>>(addedMenuItem);
        }

        public async Task<MenuItemDto> GetByID(Guid ID) {

            var menuItem = await menuItemRepository.GetByID(ID);

            return mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task<MenuItemDto> Update(MenuItem menuItem) {

            var updatedMenuItem = await menuItemRepository.Update(menuItem);

            return mapper.Map<MenuItemDto>(updatedMenuItem);
        }

        public async Task<bool> Delete(Guid ID) {

            return await menuItemRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<MenuItem> menuItem) {

            return await menuItemRepository.Delete(menuItem);
        }
    }
}
