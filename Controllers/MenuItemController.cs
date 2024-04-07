using OrderUp_API.Attributes;

namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/menu-item")]
    public class MenuItemController : ControllerBase {

        readonly MenuItemService menuItemService;
        readonly IMapper mapper;
        readonly ControllerResponseHandler responseHandler;

        public MenuItemController(MenuItemService menuItemService, IMapper mapper) {

            this.menuItemService = menuItemService;
            this.mapper = mapper;
            responseHandler = new ControllerResponseHandler();

        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetMenuItemByID(Guid ID) {

            var addedMenuItem = await menuItemService.GetByID(ID);

            if (addedMenuItem is null) return Ok(new DefaultErrorResponse<MenuItem>());


            return Ok(new DefaultSuccessResponse<MenuItemDto>(addedMenuItem));
        }



        [PermissionRequired(PermissionName.MENU_ITEM__CREATE_MENU_ITEM)]
        [HttpPost()]
        public async Task<IActionResult> AddMenuItem([FromBody] MenuItemDto menuItemDto) {

            var mappedMenuItem = mapper.Map<MenuItem>(menuItemDto);

            var addedMenuItem = await menuItemService.Save(mappedMenuItem);

            return responseHandler.HandleResponse(addedMenuItem);
        }


        [PermissionRequired(PermissionName.MENU_ITEM__UPDATE_MENU_ITEM)]
        [HttpPut()]
        public async Task<IActionResult> UpdateMenuItem(MenuItemDto menuItemDto) {

            var updatedMenuItem = await menuItemService.Update(menuItemDto);

            return responseHandler.HandleResponse(updatedMenuItem);

        }


        [PermissionRequired(PermissionName.MENU_ITEM__DELETE_MENU_ITEM)]
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteMenuItem(Guid ID) {

            var isDeletedMenuItem = await menuItemService.Delete(ID);

            if (!isDeletedMenuItem) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedMenuItem));
        }





    }
}
