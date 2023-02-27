namespace OrderUp_API.Profiles {
    public class MenuCategoryProfile : Profile {

        public MenuCategoryProfile() {

            CreateMap<MenuCategoryDto, MenuCategory>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.categoryName))
                //.ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.restaurant))
                .ForMember(dest => dest.RestaurantID, opt => opt.MapFrom(src => src.restaurantId))
                .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.menuItems));

            CreateMap<MenuCategory, MenuCategoryDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.CategoryName))
                //.ForMember(dest => dest.restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ForMember(dest => dest.restaurantId, opt => opt.MapFrom(src => src.RestaurantID))
                .ForMember(dest => dest.menuItems, opt => opt.MapFrom(src => src.MenuItems));
        }
    }
}
