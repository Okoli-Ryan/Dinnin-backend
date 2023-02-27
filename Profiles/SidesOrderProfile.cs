namespace OrderUp_API.Profiles {
    public class SidesOrderProfile : Profile {

        public SidesOrderProfile() {

            CreateMap<SidesOrderDto, SidesOrder>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
              .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt));
              

            CreateMap<SidesOrder, SidesOrderDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
        }
    }
}
