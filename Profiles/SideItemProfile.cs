namespace OrderUp_API.Profiles {
    public class SideItemProfile : Profile {

        public SideItemProfile() {

            CreateMap<SideItemDto, SideItem>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.SideItemName, opt => opt.MapFrom(src => src.sideItemName))
                .ForMember(dest => dest.SideItemPrice, opt => opt.MapFrom(src => src.sideItemPrice))
                .ForMember(dest => dest.SidesID, opt => opt.MapFrom(src => src.sidesId))
                .ForMember(dest => dest.Sides, opt => opt.MapFrom(src => src.sides));

            CreateMap<SideItem, SideItemDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.sideItemName, opt => opt.MapFrom(src => src.SideItemName))
                .ForMember(dest => dest.sideItemPrice, opt => opt.MapFrom(src => src.SideItemPrice))
                .ForMember(dest => dest.sidesId, opt => opt.MapFrom(src => src.SidesID))
                .ForMember(dest => dest.sides, opt => opt.MapFrom(src => src.Sides));
        }
    }
}
