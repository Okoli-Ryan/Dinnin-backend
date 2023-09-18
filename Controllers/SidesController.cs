namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/sides")]
    public class SidesController : ControllerBase {

        readonly SidesService sidesService;
        readonly IMapper mapper;

        public SidesController(SidesService sidesService, IMapper mapper) {

            this.sidesService = sidesService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSidesByID(Guid ID) {

            var addedSides = await sidesService.GetByID(ID);

            if (addedSides is null) return Ok(new DefaultErrorResponse<Sides>());


            return Ok(new DefaultSuccessResponse<SidesDto>(addedSides));
        }




        [HttpPost()]
        public async Task<IActionResult> AddSides([FromBody] SidesDto sidesDto) {

            DefaultResponse<SidesDto> response = new();

            var mappedSides = mapper.Map<Sides>(sidesDto);

            var addedSides = await sidesService.Save(mappedSides);

            if (addedSides is null) return Ok(new DefaultErrorResponse<SidesDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedSides;

            return Ok(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateSides([FromBody] SidesDto sidesDto) {


            var mappedSides = mapper.Map<Sides>(sidesDto);

            var updatedSides = await sidesService.Update(mappedSides);

            if (updatedSides is null) return Ok(new DefaultErrorResponse<SidesDto>());


            return Ok(new DefaultSuccessResponse<SidesDto>(updatedSides));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteSides(Guid ID) {

            var isDeletedSides = await sidesService.Delete(ID);

            if (!isDeletedSides) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedSides));
        }





    }
}
