namespace OrderUp_API.Profiles {
    public class MenuItemProfile : Profile{

        public MenuItemProfile() {

            CreateMap<MenuItemDto, MenuItem>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.menuItemName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.restaurantId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.MenuCategoryID, opt => opt.MapFrom(src => src.menuCategoryId))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.imageUrl));
                //.ForMember(dest => dest.MenuCategory, opt => opt.MapFrom(src => src.menuCategory));

            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.menuItemName, opt => opt.MapFrom(src => src.MenuItemName))
                .ForMember(dest => dest.imageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.restaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.menuCategoryId, opt => opt.MapFrom(src => src.MenuCategoryID));
                //.ForMember(dest => dest.menuCategory, opt => opt.MapFrom(src => src.MenuCategory));
        }
    }
}
