namespace OrderUp_API.Services {
    public class MenuCategoryService {

        readonly IMapper mapper;
        readonly MenuCategoryRepository menuCategoryRepository;

        public MenuCategoryService(IMapper mapper, MenuCategoryRepository menuCategoryRepository) {
            this.mapper = mapper;
            this.menuCategoryRepository = menuCategoryRepository;
        }

        public async Task<DefaultResponse<MenuCategoryDto>> Save(MenuCategory menuCategory) {

            var currentMenuItemCount = await menuCategoryRepository.GetCount(menuCategory.RestaurantID);

            menuCategory.Order = currentMenuItemCount;

            var addedMenuCategory = await menuCategoryRepository.Save(menuCategory);

            if (addedMenuCategory is null) return new DefaultErrorResponse<MenuCategoryDto>();

            var mappedResponse = mapper.Map<MenuCategoryDto>(addedMenuCategory);

            return new DefaultSuccessResponse<MenuCategoryDto>(mappedResponse);
        }

        public async Task<List<MenuCategoryDto>> Save(List<MenuCategory> menuCategory) {

            var addedMenuCategory = await menuCategoryRepository.Save(menuCategory);
            return mapper.Map<List<MenuCategoryDto>>(addedMenuCategory);
        }

        public async Task<MenuCategoryDto> GetByID(Guid ID) {

            var menuCategory = await menuCategoryRepository.GetByID(ID);

            return mapper.Map<MenuCategoryDto>(menuCategory);
        }

        public async Task<DefaultResponse<MenuCategoryDto>> Update(MenuCategory menuCategory) {

            var updatedMenuCategory = await menuCategoryRepository.Update(menuCategory);

            if (updatedMenuCategory is null) return new DefaultErrorResponse<MenuCategoryDto>();

            var mappedResponse = mapper.Map<MenuCategoryDto>(updatedMenuCategory);

            return new DefaultSuccessResponse<MenuCategoryDto>(mappedResponse);
        }
        
        public async Task<DefaultResponse<List<MenuCategoryDto>>> Update(List<MenuCategory> menuCategories) {

            var updatedMenuCategories = await menuCategoryRepository.Update(menuCategories);

            if (updatedMenuCategories is null) return new DefaultErrorResponse<List<MenuCategoryDto>>();

            var mappedResponse = mapper.Map<List<MenuCategoryDto>>(updatedMenuCategories);

            return new DefaultSuccessResponse<List<MenuCategoryDto>>(mappedResponse);
        }

        public async Task<DefaultResponse<bool>> Delete(Guid ID) {

            var response = await menuCategoryRepository.Delete(ID);

            if(!response) return new DefaultErrorResponse<bool>();

            return new DefaultSuccessResponse<bool>(response);
        }

        public async Task<bool> Delete(List<MenuCategory> menuCategory) {

            return await menuCategoryRepository.Delete(menuCategory);
        }

        public DefaultResponse<List<MenuCategoryDto>> GetMenuCategoryByRestaurantID(Guid RestaurantID) {

            var menuCategories = menuCategoryRepository.GetMenuCategoryByRestaurantID(RestaurantID);

            if(menuCategories is null) {
                return new DefaultErrorResponse<List<MenuCategoryDto>>();
            }

            var mappedResponse = mapper.Map<List<MenuCategoryDto>>(menuCategories);

            return new DefaultSuccessResponse<List<MenuCategoryDto>>(mappedResponse);
        }
    }
}
