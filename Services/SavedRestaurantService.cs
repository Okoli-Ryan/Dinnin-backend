namespace OrderUp_API.Services {
    public class SavedRestaurantService {

        readonly IMapper mapper;
        readonly SavedRestaurantRepository savedRestaurantRepository;

        public SavedRestaurantService(IMapper mapper, SavedRestaurantRepository savedRestaurantRepository) {
            this.mapper = mapper;
            this.savedRestaurantRepository = savedRestaurantRepository;
        }

        public async Task<SavedRestaurantDto> Save(SavedRestaurant savedRestaurant) {

            var addedSavedRestaurant = await savedRestaurantRepository.Save(savedRestaurant);
            return mapper.Map<SavedRestaurantDto>(addedSavedRestaurant);
        }

        public async Task<List<SavedRestaurantDto>> Save(List<SavedRestaurant> savedRestaurant) {

            var addedSavedRestaurant = await savedRestaurantRepository.Save(savedRestaurant);
            return mapper.Map<List<SavedRestaurantDto>>(addedSavedRestaurant);
        }

        public async Task<SavedRestaurantDto> GetByID(Guid ID) {

            var savedRestaurant = await savedRestaurantRepository.GetByID(ID);

            return mapper.Map<SavedRestaurantDto>(savedRestaurant);
        }

        public async Task<SavedRestaurantDto> Update(SavedRestaurant savedRestaurant) {

            var updatedSavedRestaurant = await savedRestaurantRepository.Update(savedRestaurant);

            return mapper.Map<SavedRestaurantDto>(updatedSavedRestaurant);
        }

        public async Task<bool> Delete(Guid ID) {

            return await savedRestaurantRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<SavedRestaurant> savedRestaurant) {

            return await savedRestaurantRepository.Delete(savedRestaurant);
        }
    }
}
