using OrderUp_API.Repository;

namespace OrderUp_API.Services {
    public class RestaurantService {

        readonly IMapper mapper;
        readonly RestaurantRepository restaurantRepository;
        readonly AdminRepository adminRepository;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly OrderUpDbContext context;

        public RestaurantService(IMapper mapper, RestaurantRepository restaurantRepository, IHttpContextAccessor httpContextAccessor, OrderUpDbContext context, AdminRepository adminRepository) {
            this.mapper = mapper;
            this.restaurantRepository = restaurantRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.adminRepository = adminRepository;
        }

        public async Task<RestaurantDto> GetRestaurantBySlug(string Slug) {
            var RestaurantData = await restaurantRepository.GetRestuarantDetailsBySlug(Slug);


            var mappedRestaurantResponse = mapper.Map<RestaurantDto>(RestaurantData);

            return mappedRestaurantResponse;
        }

        public async Task<bool> DoesSlugExist(string Slug) {
            var DoesRestaurantSlugExist = await restaurantRepository.DoesSlugExist(Slug);

            return DoesRestaurantSlugExist;
        }

        public async Task<DefaultResponse<RestaurantDto>> Save(RestaurantDto restaurantDto) {

            var mappedAddedRestaurant = mapper.Map<Restaurant>(restaurantDto);

            var contextAccessor = httpContextAccessor.HttpContext;

            using var transaction = context.Database.BeginTransaction();


            var addedRestaurant = await restaurantRepository.Save(mappedAddedRestaurant);

            if (addedRestaurant is null) return new DefaultErrorResponse<RestaurantDto>();

            string AdminId = GetJwtValue.GetValueFromBearerToken(contextAccessor, ClaimTypes.PrimarySid);

            if (AdminId is null) return new DefaultErrorResponse<RestaurantDto>() { 
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                ResponseMessage = ResponseMessages.UNAUTHORIZED,
                ResponseData = null
            };


            var AuthorizedAdmin = await adminRepository.GetByID(Guid.Parse(AdminId));

            if (AuthorizedAdmin is null) return new DefaultErrorResponse<RestaurantDto>();

            AuthorizedAdmin.RestaurantID = addedRestaurant.ID;

            var updatedAuthorizedAdmin = await adminRepository.Update(AuthorizedAdmin);

            if (updatedAuthorizedAdmin is null) return new DefaultErrorResponse<RestaurantDto>();


            transaction.Commit();


            var response = mapper.Map<RestaurantDto>(addedRestaurant);

            return new DefaultSuccessResponse<RestaurantDto>(response);
        }


        public async Task<RestaurantDto> GetByID(Guid ID) {

            var restaurant = await restaurantRepository.GetByID(ID);

            return mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<DefaultResponse<RestaurantDto>> Update(RestaurantDto restaurantDto) {

            var mappedRestaurant = mapper.Map<Restaurant>(restaurantDto);

            var updatedRestaurant = await restaurantRepository.Update(mappedRestaurant);

            if (updatedRestaurant is null) return new DefaultErrorResponse<RestaurantDto>();

            var mappedUpdatedRestaurant = mapper.Map<RestaurantDto>(updatedRestaurant);

            return new DefaultSuccessResponse<RestaurantDto>(mappedUpdatedRestaurant);
        }

        public async Task<bool> Delete(Guid ID) {

            return await restaurantRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Restaurant> restaurant) {

            return await restaurantRepository.Delete(restaurant);
        }
    }
}
