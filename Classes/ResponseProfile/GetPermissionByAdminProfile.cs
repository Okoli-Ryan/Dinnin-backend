using OrderUp_API.Classes.ResponseDtos;
using OrderUp_API.Classes.ResponseModels;

namespace OrderUp_API.Classes.ResponseProfile {
    public class GetPermissionByAdminProfile : Profile {

        public GetPermissionByAdminProfile() {

            CreateMap<GetPermissionsByAdminResponse, GetPermissionsByAdminDto>()
                .ForMember(dest => dest.adminName, opt => opt.MapFrom(opt => opt.AdminName))
                .ForMember(dest => dest.permissionGroups, opt => opt.MapFrom(opt => opt.PermissionGroups));
        }
    }
}
