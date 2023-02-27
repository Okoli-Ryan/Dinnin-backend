namespace OrderUp_API.Profiles {
    public class TableProfile : Profile{

        public TableProfile() {

            CreateMap<TableDto, Table>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.tableName))
                .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.restaurant))
                .ForMember(dest => dest.RestaurantID, opt => opt.MapFrom(src => src.restaurantId));

            CreateMap<Table, TableDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.tableName, opt => opt.MapFrom(src => src.TableName))
                .ForMember(dest => dest.restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ForMember(dest => dest.restaurantId, opt => opt.MapFrom(src => src.RestaurantID));
        }
    }
}
