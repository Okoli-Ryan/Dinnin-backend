namespace OrderUp_API.Services {
    public class SidesOrderService {

        private readonly SidesOrderRepository sidesOrderRepository;
        private readonly IMapper mapper;

        public SidesOrderService(SidesOrderRepository sidesOrderRepository, IMapper mapper) {
            this.sidesOrderRepository = sidesOrderRepository;
            this.mapper = mapper;
        }

        public async Task<SidesOrderDto> Save(SidesOrder sidesOrder) {

            var addedSidesOrder = await sidesOrderRepository.Save(sidesOrder);

            return mapper.Map<SidesOrderDto>(addedSidesOrder);
        }

        public async Task<List<SidesOrderDto>> Save(List<SidesOrder> sidesOrder) {

            var addedSidesOrder = await sidesOrderRepository.Save(sidesOrder);

            return mapper.Map<List<SidesOrderDto>>(addedSidesOrder);
        }

        public async Task<SidesOrderDto> GetByID(Guid ID) {

            var sidesOrder = await sidesOrderRepository.GetByID(ID);

            return mapper.Map<SidesOrderDto>(sidesOrder);
        }

        public async Task<SidesOrderDto> Update(SidesOrder sidesOrder) {

            var updatedSidesOrder = await sidesOrderRepository.Update(sidesOrder);

            return mapper.Map<SidesOrderDto>(updatedSidesOrder);
        }

        public async Task<bool> Delete(Guid ID) {

            return await sidesOrderRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<SidesOrder> sidesOrder) {

            return await sidesOrderRepository.Delete(sidesOrder);
        }

    }
}
