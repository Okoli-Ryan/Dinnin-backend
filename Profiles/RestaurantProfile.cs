namespace OrderUp_API.Profiles {
    public class RestaurantProfile : Profile {

        public RestaurantProfile() {

            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.state))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.logoUrl))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.restaurantName))
                .ForMember(dest => dest.XCoordinate, opt => opt.MapFrom(src => src.xCoordinate))
                .ForMember(dest => dest.YCoordinate, opt => opt.MapFrom(src => src.yCoordinate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.MenuCategories, opt => opt.MapFrom(src => src.categories))
                .ForMember(dest => dest.ContactEmailAddress, opt => opt.MapFrom(src => src.contactEmail))
                .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.contactPhoneNumber));

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.contactPhoneNumber, opt => opt.MapFrom(src => src.ContactPhoneNumber))
                .ForMember(dest => dest.city, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.logoUrl, opt => opt.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.contactEmail, opt => opt.MapFrom(src => src.ContactEmailAddress))
                .ForMember(dest => dest.state, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.restaurantName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.xCoordinate, opt => opt.MapFrom(src => src.XCoordinate))
                .ForMember(dest => dest.yCoordinate, opt => opt.MapFrom(src => src.YCoordinate))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.categories, opt => opt.MapFrom(src => src.MenuCategories));

        }
    }
}
