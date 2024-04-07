namespace OrderUp_API.Profiles {
    public class PermissionProfile : Profile {

        public PermissionProfile() {

            CreateMap<PermissionDto, Permission>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.permissionName))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.permissionDescription))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.permissionCategory))
                .ForMember(dest => dest.Admins, opt => opt.MapFrom(src => src.admins))
                .ForMember(dest => dest.AdminPermissions, opt => opt.MapFrom(src => src.adminPermissions));

            CreateMap<Permission, PermissionDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.permissionName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.permissionDescription, opt => opt.MapFrom(src => src.Alias))
                .ForMember(dest => dest.permissionCategory, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.admins, opt => opt.Ignore())
                .ForMember(dest => dest.adminPermissions, opt => opt.Ignore());
        }
    }
}
