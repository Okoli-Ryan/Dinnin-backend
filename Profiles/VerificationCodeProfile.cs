namespace OrderUp_API.Profiles {
    public class VerificationCodeProfile : Profile {

        public VerificationCodeProfile() {

            CreateMap<VerificationCodeDto, VerificationCode>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.expiryDate))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.code))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.userType))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.userId));

            CreateMap<VerificationCode, VerificationCodeDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.expiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.userType, opt => opt.MapFrom(src => src.UserType))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserID));
        }
    }
}
