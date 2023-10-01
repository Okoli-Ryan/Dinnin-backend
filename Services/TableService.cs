using OrderUp_API.Constants;

namespace OrderUp_API.Services {
    public class TableService {

        readonly IMapper mapper;
        readonly TableRepository tableRepository;

        public TableService(IMapper mapper, TableRepository tableRepository) {
            this.mapper = mapper;
            this.tableRepository = tableRepository;
        }

        public async Task<TableDto> GetTableData(Guid TableID) {

            var TableData = await tableRepository.GetTableData(TableID);

            return mapper.Map<TableDto>(TableData);
            
        }

        public async Task<DefaultResponse<TableDto>> GenerateTableCode(Guid ID) {

            var table = await tableRepository.GetByID(ID);

            if (table is null) return new DefaultErrorResponse<TableDto>() { 
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseMessage = ResponseMessages.NOT_FOUND,
                ResponseData = null
            };

            table.Code = RandomStringGenerator.GenerateRandomString(TableModelConstants.TableCodeLength);

            return await Update(table);

        }




        public async Task<DefaultResponse<List<TableDto>>> GetTablesByRestaurantID(Guid RestaurantID) {

            var Tables = await tableRepository.GetTablesByRestaurantID(RestaurantID);

            if (Tables is null) return new DefaultErrorResponse<List<TableDto>>() {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseMessage = ResponseMessages.NOT_FOUND,
                ResponseData = null
            };

            var mappedResponse = mapper.Map<List<TableDto>>(Tables);

            return new DefaultSuccessResponse<List<TableDto>>(mappedResponse);
        }

        public async Task<DefaultResponse<TableDto>> Save(Table table) {

            table.Code = RandomStringGenerator.GenerateRandomString(TableModelConstants.TableCodeLength);

            var addedTable = await tableRepository.Save(table);

            if (addedTable is null) return new DefaultErrorResponse<TableDto>();

            var mappedResponse = mapper.Map<TableDto>(addedTable);

            return new DefaultSuccessResponse<TableDto>(mappedResponse);
        }

        public async Task<List<TableDto>> Save(List<Table> table) {

            var addedTable = await tableRepository.Save(table);
            return mapper.Map<List<TableDto>>(addedTable);
        }

        public async Task<DefaultResponse<TableDto>> GetByID(Guid ID) {

            var table = await tableRepository.GetByID(ID);

            if (table is null) return new DefaultErrorResponse<TableDto>() {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseMessage = ResponseMessages.NOT_FOUND,
                ResponseData = null
            };

            var mappedResponse = mapper.Map<TableDto>(table);

            return new DefaultSuccessResponse<TableDto>(mappedResponse);
        }

        public async Task<DefaultResponse<TableDto>> Update(Table table) {

            var updatedTable = await tableRepository.Update(table);

            if (updatedTable is null) return new DefaultErrorResponse<TableDto>();

            var mappedResponse = mapper.Map<TableDto>(updatedTable);

            return new DefaultSuccessResponse<TableDto>(mappedResponse);
        }

        public async Task<bool> Delete(Guid ID) {

            return await tableRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Table> table) {

            return await tableRepository.Delete(table);
        }
    }
}
