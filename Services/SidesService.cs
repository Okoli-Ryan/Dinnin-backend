namespace OrderUp_API.Services {
    public class SidesService {

        readonly IMapper mapper;
        readonly SidesRepository sideRepository;

        public SidesService(IMapper mapper, SidesRepository sideRepository) {
            this.mapper = mapper;
            this.sideRepository = sideRepository;
        }

        public async Task<SidesDto> Save(Sides side) {

            var addedSides = await sideRepository.Save(side);
            return mapper.Map<SidesDto>(addedSides);
        }

        public async Task<List<SidesDto>> Save(List<Sides> side) {

            var addedSides = await sideRepository.Save(side);
            return mapper.Map<List<SidesDto>>(addedSides);
        }

        public async Task<SidesDto> GetByID(Guid ID) {

            var side = await sideRepository.GetByID(ID);

            return mapper.Map<SidesDto>(side);
        }

        public async Task<SidesDto> Update(Sides side) {

            var updatedSides = await sideRepository.Update(side);

            return mapper.Map<SidesDto>(updatedSides);
        }

        public async Task<bool> Delete(Guid ID) {

            return await sideRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Sides> side) {

            return await sideRepository.Delete(side);
        }
    }
}
