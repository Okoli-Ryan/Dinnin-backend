﻿namespace OrderUp_API.Profiles {
    public class AdminProfile : Profile {

        public AdminProfile() {

            CreateMap<AdminDto, Admin>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.RestaurantID, opt => opt.MapFrom(src => src.restaurantId))
                .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.restaurant))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.role))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.firstName))
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.isEmailConfirmed))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.lastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.emailAddress))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.RecoveryEmail, opt => opt.MapFrom(src => src.recoveryEmail))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.position))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phoneNumber));

            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ForMember(dest => dest.isEmailConfirmed, opt => opt.MapFrom(src => src.IsEmailConfirmed))
                .ForMember(dest => dest.restaurantId, opt => opt.MapFrom(src => src.RestaurantID))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.password, opt => opt.Ignore())
                .ForMember(dest => dest.emailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.recoveryEmail, opt => opt.MapFrom(src => src.RecoveryEmail))
                .ForMember(dest => dest.position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
