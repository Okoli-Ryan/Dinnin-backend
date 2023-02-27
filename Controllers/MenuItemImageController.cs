namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/menu-item-image")]
    public class MenuItemImageController : ControllerBase {

        readonly MenuItemImageService menuItemImageService;
        readonly IMapper mapper;

        public MenuItemImageController(MenuItemImageService menuItemImageService, IMapper mapper) {

            this.menuItemImageService = menuItemImageService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetMenuItemImageByID(Guid ID) {

            var addedMenuItemImage = await menuItemImageService.GetByID(ID);

            if (addedMenuItemImage is null) return Ok(new DefaultErrorResponse<MenuItemImage>());


            return Ok(new DefaultSuccessResponse<MenuItemImageDto>(addedMenuItemImage));
        }




        [HttpPost()]
        public async Task<IActionResult> AddMenuItemImage([FromBody] MenuItemImageDto menuItemImageDto) {

            DefaultResponse<MenuItemImageDto> response = new();

            var mappedMenuItemImage = mapper.Map<MenuItemImage>(menuItemImageDto);

            var addedMenuItemImage = await menuItemImageService.Save(mappedMenuItemImage);

            if (addedMenuItemImage is null) return Ok(new DefaultErrorResponse<MenuItemImageDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedMenuItemImage;

            return Ok(response);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateMenuItemImage([FromBody] MenuItemImageDto menuItemImageDto) {


            var mappedMenuItemImage = mapper.Map<MenuItemImage>(menuItemImageDto);

            var updatedMenuItemImage = await menuItemImageService.Update(mappedMenuItemImage);

            if (updatedMenuItemImage is null) return Ok(new DefaultErrorResponse<MenuItemImageDto>());


            return Ok(new DefaultSuccessResponse<MenuItemImageDto>(updatedMenuItemImage));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteMenuItemImage(Guid ID) {

            var isDeletedMenuItemImage = await menuItemImageService.Delete(ID);

            if (!isDeletedMenuItemImage) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedMenuItemImage));
        }





    }
}
