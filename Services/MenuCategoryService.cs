namespace OrderUp_API.Services {
    public class MenuCategoryService {

        readonly IMapper mapper;
        readonly MenuCategoryRepository menuCategoryRepository;

        public MenuCategoryService(IMapper mapper, MenuCategoryRepository menuCategoryRepository) {
            this.mapper = mapper;
            this.menuCategoryRepository = menuCategoryRepository;
        }

        public async Task<MenuCategoryDto> Save(MenuCategory menuCategory) {

            var addedMenuCategory = await menuCategoryRepository.Save(menuCategory);
            return mapper.Map<MenuCategoryDto>(addedMenuCategory);
        }

        public async Task<List<MenuCategoryDto>> Save(List<MenuCategory> menuCategory) {

            var addedMenuCategory = await menuCategoryRepository.Save(menuCategory);
            return mapper.Map<List<MenuCategoryDto>>(addedMenuCategory);
        }

        public async Task<MenuCategoryDto> GetByID(Guid ID) {

            var menuCategory = await menuCategoryRepository.GetByID(ID);

            return mapper.Map<MenuCategoryDto>(menuCategory);
        }

        public async Task<MenuCategoryDto> Update(MenuCategory menuCategory) {

            var updatedMenuCategory = await menuCategoryRepository.Update(menuCategory);

            return mapper.Map<MenuCategoryDto>(updatedMenuCategory);
        }

        public async Task<bool> Delete(Guid ID) {

            return await menuCategoryRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<MenuCategory> menuCategory) {

            return await menuCategoryRepository.Delete(menuCategory);
        }
    }
}
