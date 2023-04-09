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

        public async Task<TableDto> Save(Table table) {

            var addedTable = await tableRepository.Save(table);
            return mapper.Map<TableDto>(addedTable);
        }

        public async Task<List<TableDto>> Save(List<Table> table) {

            var addedTable = await tableRepository.Save(table);
            return mapper.Map<List<TableDto>>(addedTable);
        }

        public async Task<TableDto> GetByID(Guid ID) {

            var table = await tableRepository.GetByID(ID);

            return mapper.Map<TableDto>(table);
        }

        public async Task<TableDto> Update(Table table) {

            var updatedTable = await tableRepository.Update(table);

            return mapper.Map<TableDto>(updatedTable);
        }

        public async Task<bool> Delete(Guid ID) {

            return await tableRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Table> table) {

            return await tableRepository.Delete(table);
        }
    }
}
