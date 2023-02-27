namespace OrderUp_API.Services {
    public class MenuItemImageService {

        readonly IMapper mapper;
        readonly MenuItemImageRepository menuItemImageRepository;

        public MenuItemImageService(IMapper mapper, MenuItemImageRepository menuItemImageRepository) {
            this.mapper = mapper;
            this.menuItemImageRepository = menuItemImageRepository;
        }

        public async Task<MenuItemImageDto> Save(MenuItemImage menuItemImage) {

            var addedMenuItemImage = await menuItemImageRepository.Save(menuItemImage);
            return mapper.Map<MenuItemImageDto>(addedMenuItemImage);
        }

        public async Task<List<MenuItemImageDto>> Save(List<MenuItemImage> menuItemImage) {

            var addedMenuItemImage = await menuItemImageRepository.Save(menuItemImage);
            return mapper.Map<List<MenuItemImageDto>>(addedMenuItemImage);
        }

        public async Task<MenuItemImageDto> GetByID(Guid ID) {

            var menuItemImage = await menuItemImageRepository.GetByID(ID);

            return mapper.Map<MenuItemImageDto>(menuItemImage);
        }

        public async Task<MenuItemImageDto> Update(MenuItemImage menuItemImage) {

            var updatedMenuItemImage = await menuItemImageRepository.Update(menuItemImage);

            return mapper.Map<MenuItemImageDto>(updatedMenuItemImage);
        }

        public async Task<bool> Delete(Guid ID) {

            return await menuItemImageRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<MenuItemImage> menuItemImage) {

            return await menuItemImageRepository.Delete(menuItemImage);
        }
    }
}
