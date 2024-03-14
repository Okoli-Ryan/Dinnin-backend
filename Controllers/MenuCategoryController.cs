namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/menu-category")]
    public class MenuCategoryController : ControllerBase {

        readonly MenuCategoryService menuCategoryService;
        readonly ControllerResponseHandler responseHandler;
        readonly IMapper mapper;

        public MenuCategoryController(MenuCategoryService menuCategoryService, IMapper mapper) {

            this.menuCategoryService = menuCategoryService;
            this.mapper = mapper;
            responseHandler = new ControllerResponseHandler();
        }

        [HttpGet("rid/{RestaurantID}")]
        public IActionResult GetMenuCategoryByRestaurantID(Guid RestaurantID) {

            var menuCategoies = menuCategoryService.GetMenuCategoryByRestaurantID(RestaurantID);

            return responseHandler.HandleResponse(menuCategoies);
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetMenuCategoryByID(Guid ID) {

            var addedMenuCategory = await menuCategoryService.GetByID(ID);

            if (addedMenuCategory is null) return Ok(new DefaultErrorResponse<MenuCategory>());


            return Ok(new DefaultSuccessResponse<MenuCategoryDto>(addedMenuCategory));
        }




        [HttpPost()]
        public async Task<IActionResult> AddMenuCategory([FromBody] MenuCategoryDto menuCategoryDto) {

            var mappedMenuCategory = mapper.Map<MenuCategory>(menuCategoryDto);

            var addedMenuCategory = await menuCategoryService.Save(mappedMenuCategory);

            return responseHandler.HandleResponse(addedMenuCategory);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateMenuCategory([FromBody] MenuCategoryDto menuCategoryDto) {

            var mappedMenuCategory = mapper.Map<MenuCategory>(menuCategoryDto);

            var updatedMenuCategory = await menuCategoryService.Update(mappedMenuCategory);

            return responseHandler.HandleResponse(updatedMenuCategory);

        }


        [HttpPut("multiple")]
        public async Task<IActionResult> UpdateMenuCategoryList([FromBody] List<MenuCategoryDto> menuCategoryDto) {

            var mappedMenuCategory = mapper.Map<List<MenuCategory>>(menuCategoryDto);

            var updatedMenuCategory = await menuCategoryService.Update(mappedMenuCategory);

            return responseHandler.HandleResponse(updatedMenuCategory);

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteMenuCategory(Guid ID) {

            var isDeletedMenuCategory = await menuCategoryService.Delete(ID);

            return responseHandler.HandleResponse(isDeletedMenuCategory);
        }





    }
}
