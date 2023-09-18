namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/table")]
    public class TableController : ControllerBase {

        readonly TableService tableService;
        readonly IMapper mapper;

        public TableController(TableService tableService, IMapper mapper) {

            this.tableService = tableService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetTableData(Guid ID) {

            var Table = await tableService.GetTableData(ID);

            if (Table is null) return BadRequest(Table);


            return Ok(Table);
        }



        [HttpGet("s/{ID}")]
        public async Task<IActionResult> GetTableByID(Guid ID) {

            var Table = await tableService.GetByID(ID);

            if (Table is null) return Ok(new DefaultErrorResponse<Table>());


            return Ok(new DefaultSuccessResponse<TableDto>(Table));
        }




        [HttpPost()]
        public async Task<IActionResult> AddTable([FromBody] TableDto tableDto) {

            DefaultResponse<TableDto> response = new();

            var mappedTable = mapper.Map<Table>(tableDto);

            var addedTable = await tableService.Save(mappedTable);

            if (addedTable is null) return Ok(new DefaultErrorResponse<TableDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedTable;

            return Ok(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateTable([FromBody] TableDto tableDto) {


            var mappedTable = mapper.Map<Table>(tableDto);

            var updatedTable = await tableService.Update(mappedTable);

            if (updatedTable is null) return Ok(new DefaultErrorResponse<TableDto>());


            return Ok(new DefaultSuccessResponse<TableDto>(updatedTable));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTable(Guid ID) {

            var isDeletedTable = await tableService.Delete(ID);

            if (!isDeletedTable) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedTable));
        }





    }
}
