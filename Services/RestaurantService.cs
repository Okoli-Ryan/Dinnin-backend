namespace OrderUp_API.Services {
    public class RestaurantService {

        readonly IMapper mapper;
        readonly RestaurantRepository restaurantRepository;

        public RestaurantService(IMapper mapper, RestaurantRepository restaurantRepository) {
            this.mapper = mapper;
            this.restaurantRepository = restaurantRepository;
        }

        public async Task<RestaurantDto> GetRestaurantBySlug(string Slug) {
            var RestaurantData = await restaurantRepository.GetRestuarantDetailsBySlug(Slug);

            
            var mappedRestaurantResponse = mapper.Map<RestaurantDto>(RestaurantData);

            return mappedRestaurantResponse;
        }

        public async Task<RestaurantDto> Save(Restaurant restaurant) {

            var addedRestaurant = await restaurantRepository.Save(restaurant);
            return mapper.Map<RestaurantDto>(addedRestaurant);
        }

        public async Task<List<RestaurantDto>> Save(List<Restaurant> restaurant) {

            var addedRestaurant = await restaurantRepository.Save(restaurant);
            return mapper.Map<List<RestaurantDto>>(addedRestaurant);
        }

        public async Task<RestaurantDto> GetByID(Guid ID) {

            var restaurant = await restaurantRepository.GetByID(ID);

            return mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto> Update(Restaurant restaurant) {

            var updatedRestaurant = await restaurantRepository.Update(restaurant);

            return mapper.Map<RestaurantDto>(updatedRestaurant);
        }

        public async Task<bool> Delete(Guid ID) {

            return await restaurantRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Restaurant> restaurant) {

            return await restaurantRepository.Delete(restaurant);
        }
    }
}
