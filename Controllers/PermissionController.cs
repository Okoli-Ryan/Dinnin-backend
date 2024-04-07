using OrderUp_API.Attributes;

namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/permission")]
    public class PermissionController : ControllerBase {

        readonly PermissionService _permissionService;
        readonly ControllerResponseHandler _responseHandler;

        public PermissionController(PermissionService service) {

            _permissionService = service;
            _responseHandler = new ControllerResponseHandler();
        }

        [PermissionRequired(PermissionName.PERMISSIONS__VIEW_PERMISSIONS)]
        [HttpGet]
        public async Task<IActionResult> GetPermissions() { 
        
            var response = await _permissionService.GetPermissions();

            return _responseHandler.HandleResponse(response);

        }
    }
}
