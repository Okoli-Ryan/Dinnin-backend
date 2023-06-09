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
        public IActionResult Upload([FromForm(Name = "image")] IFormFile file, [FromForm] string folderName) {

            var response = imageUploadService.Upload(file, folderName);

            return ResponseHandler.HandleResponse(response);
        } 
    }
}
