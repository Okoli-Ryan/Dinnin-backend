using OrderUp_API.Classes.RequestModels;

namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/image")]
    public class ImageUploadController : ControllerBase {

        readonly CloudinaryService imageUploadService;
        readonly ControllerResponseHandler ResponseHandler;

        public ImageUploadController(CloudinaryService imageUploadService) {
            this.imageUploadService = imageUploadService;
            ResponseHandler = new ControllerResponseHandler();

        }

        [HttpPost]
        public IActionResult Upload([FromForm] FileUploadRequest fileBody) {


            var response = imageUploadService.Upload(fileBody.file, fileBody.folderName);

            return ResponseHandler.HandleResponse(response);
        }

    }
}
