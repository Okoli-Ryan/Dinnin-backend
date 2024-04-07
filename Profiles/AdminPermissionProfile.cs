namespace OrderUp_API.Profiles {
    public class AdminPermissionProfile : Profile {

        public AdminPermissionProfile() {

            CreateMap<AdminPermissionDto, AdminPermission>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src => src.adminId))
                .ForMember(dest => dest.PermissionID, opt => opt.MapFrom(src => src.permissionId))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => src.admin))
                .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.permission));

            CreateMap<AdminPermission, AdminPermissionDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.adminId, opt => opt.MapFrom(src => src.AdminID))
                .ForMember(dest => dest.permissionId, opt => opt.MapFrom(src => src.PermissionID))
                .ForMember(dest => dest.admin, opt => opt.MapFrom(src => src.Admin))
                .ForMember(dest => dest.permission, opt => opt.MapFrom(src => src.Permission));
        }
    }
}
