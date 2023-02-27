namespace OrderUp_API.Profiles {
    public class UserProfile : Profile {

        public UserProfile() {

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.lastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.emailAddress))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.imageUrl))
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.isEmailConfirmed))
                .ForMember(dest => dest.SavedRestaurants, opt => opt.MapFrom(src => src.savedRestaurants));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.emailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.isEmailConfirmed, opt => opt.MapFrom(src => src.IsEmailConfirmed))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.imageUrl, opt => opt.MapFrom(src => src.UserImageUrl))
                .ForMember(dest => dest.savedRestaurants, opt => opt.MapFrom(src => src.SavedRestaurants));
        }
    }
}
