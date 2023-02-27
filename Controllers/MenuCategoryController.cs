namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/menu-category")]
    public class MenuCategoryController: ControllerBase {

        readonly MenuCategoryService menuCategoryService;
        readonly IMapper mapper;

        public MenuCategoryController(MenuCategoryService menuCategoryService, IMapper mapper) { 
        
            this.menuCategoryService = menuCategoryService;
            this.mapper = mapper;
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetMenuCategoryByID(Guid ID) {

            var addedMenuCategory = await menuCategoryService.GetByID(ID);

            if (addedMenuCategory is null) return Ok(new DefaultErrorResponse<MenuCategory>());


            return Ok(new DefaultSuccessResponse<MenuCategoryDto>(addedMenuCategory));
        }




        [HttpPost()]
        public async Task<IActionResult> AddMenuCategory([FromBody] MenuCategoryDto menuCategoryDto) {

            DefaultResponse<MenuCategoryDto> response = new();

            var mappedMenuCategory = mapper.Map<MenuCategory>(menuCategoryDto);

            var addedMenuCategory = await menuCategoryService.Save(mappedMenuCategory);

            if (addedMenuCategory is null) return Ok(new DefaultErrorResponse<MenuCategoryDto>());

            response.ResponseCode = ResponseCodes.SUCCESS;
            response.ResponseMessage = ResponseMessages.SUCCESS;
            response.ResponseData = addedMenuCategory;

            return Ok(response);
        }



        [HttpPatch()]
        public async Task<IActionResult> UpdateMenuCategory([FromBody] MenuCategoryDto menuCategoryDto) {


            var mappedMenuCategory = mapper.Map<MenuCategory>(menuCategoryDto);

            var updatedMenuCategory = await menuCategoryService.Update(mappedMenuCategory);

            if (updatedMenuCategory is null) return Ok(new DefaultErrorResponse<MenuCategoryDto>());


            return Ok(new DefaultSuccessResponse<MenuCategoryDto>(updatedMenuCategory));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteMenuCategory(Guid ID) {

            var isDeletedMenuCategory = await menuCategoryService.Delete(ID);

            if (!isDeletedMenuCategory) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedMenuCategory));
        }





    }
}
