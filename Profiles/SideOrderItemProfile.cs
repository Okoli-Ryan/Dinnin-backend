namespace OrderUp_API.Profiles {
    public class SideOrderItemProfile : Profile{

        public SideOrderItemProfile() {

            CreateMap<SideOrderItemDto, SideOrderItem>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
              .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
              .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity))
              .ForMember(dest => dest.SideItemID, opt => opt.MapFrom(src => src.sideItemId))
              .ForMember(dest => dest.SideOrderID, opt => opt.MapFrom(src => src.sideOrderId))
              .ForMember(dest => dest.SideItem, opt => opt.MapFrom(src => src.sideItem))
              .ForMember(dest => dest.SidesOrder, opt => opt.MapFrom(src => src.sidesOrder))
              ;


            CreateMap<SideOrderItem, SideOrderItemDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.sideItemId, opt => opt.MapFrom(src => src.SideItemID))
                .ForMember(dest => dest.sideOrderId, opt => opt.MapFrom(src => src.SideOrderID))
                .ForMember(dest => dest.sideItem, opt => opt.MapFrom(src => src.SideItem))
                .ForMember(dest => dest.sidesOrder, opt => opt.MapFrom(src => src.SidesOrder));
                
        }
    }
}
