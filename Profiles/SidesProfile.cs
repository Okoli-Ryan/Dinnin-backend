namespace OrderUp_API.Profiles {
    public class SidesProfile : Profile{

        public SidesProfile() {

            CreateMap<SidesDto, Sides>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.MenuItemID, opt => opt.MapFrom(src => src.menuItemId))
                .ForMember(dest => dest.MenuItem, opt => opt.MapFrom(src => src.menuItem))
                .ForMember(dest => dest.SideItems, opt => opt.MapFrom(src => src.sideItems));

            CreateMap<Sides, SidesDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.menuItemId, opt => opt.MapFrom(src => src.MenuItemID))
                .ForMember(dest => dest.menuItem, opt => opt.MapFrom(src => src.MenuItem))
                .ForMember(dest => dest.sideItems, opt => opt.MapFrom(src => src.SideItems));
        }
    }
}
