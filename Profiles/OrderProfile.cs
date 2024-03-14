namespace OrderUp_API.Profiles {
    public class OrderProfile : Profile {

        public OrderProfile() {

            CreateMap<OrderDto, Order>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
              .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.activeStatus))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt))
              .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updatedAt))
              .ForMember(dest => dest.OrderNote, opt => opt.MapFrom(src => src.orderNote))
              .ForMember(dest => dest.OrderAmount, opt => opt.MapFrom(src => src.price))
              .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.orderStatus))
              .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.restaurantId))
              .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.orderItems))
              .ForMember(dest => dest.TableID, opt => opt.MapFrom(src => src.tableId))
              .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.table))
              .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.userId));


            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.ActiveStatus))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.orderNote, opt => opt.MapFrom(src => src.OrderNote))
                .ForMember(dest => dest.orderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(dest => dest.restaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.table, opt => opt.MapFrom(src => src.Table))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.OrderAmount))
                .ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.tableId, opt => opt.MapFrom(src => src.TableID))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserID));
        }
    }
}
