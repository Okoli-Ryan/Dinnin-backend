namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/table")]
    public class TableController : ControllerBase {

        readonly TableService tableService;
        readonly ControllerResponseHandler responseHandler;
        readonly IMapper mapper;

        public TableController(TableService tableService, IMapper mapper) {

            this.tableService = tableService;
            this.mapper = mapper;
            responseHandler = new ControllerResponseHandler();
        }

        [HttpGet("rid/{ID}")]
        public async Task<IActionResult> GetTablesByRestaurantID(Guid ID) {

            var TablesResponse = await tableService.GetTablesByRestaurantID(ID);

            return responseHandler.HandleResponse(TablesResponse);
        }

        [HttpPut("generate-code/{ID}")]
        public async Task<IActionResult> GenerateTableCode(Guid ID) {

            var TableResponse = await tableService.GenerateTableCode(ID);

            return responseHandler.HandleResponse(TableResponse);
        }


        [HttpGet("data/{TableCode}")]
        public async Task<IActionResult> GetTableData(string TableCode) {

            var TableResponse = await tableService.GetTableData(TableCode);

            return responseHandler.HandleResponse(TableResponse);
        }



        [HttpGet("{ID}")]
        public async Task<IActionResult> GetTableByID(Guid ID) {

            var TableResponse = await tableService.GetByID(ID);

            return responseHandler.HandleResponse(TableResponse);
        }




        [HttpPost()]
        public async Task<IActionResult> AddTable([FromBody] TableDto tableDto) {

            var mappedTable = mapper.Map<Table>(tableDto);

            var addedTableResponse = await tableService.Save(mappedTable);

            return responseHandler.HandleResponse(addedTableResponse);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateTable([FromBody] TableDto tableDto) {

            var mappedTable = mapper.Map<Table>(tableDto);

            var updatedTableResponse = await tableService.Update(mappedTable);

            return responseHandler.HandleResponse(updatedTableResponse);

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTable(Guid ID) {

            var DeletedTableResponse = await tableService.Delete(ID);

            return responseHandler.HandleResponse(DeletedTableResponse);
        }





    }
}
