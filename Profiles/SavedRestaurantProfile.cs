namespace OrderUp_API.Profiles {
    public class SavedRestaurantProfile : Profile{

        public SavedRestaurantProfile() {

            CreateMap<SavedRestaurantDto, SavedRestaurant>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.restaurant))
                .ForMember(dest => dest.RestaurantID, opt => opt.MapFrom(src => src.restaurantId))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.user))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.userId));

            CreateMap<SavedRestaurant, SavedRestaurantDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ForMember(dest => dest.restaurantId, opt => opt.MapFrom(src => src.RestaurantID))
                .ForMember(dest => dest.user, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserID));
        }
    }
}
