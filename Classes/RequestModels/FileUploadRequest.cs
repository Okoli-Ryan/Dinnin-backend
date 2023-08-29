namespace OrderUp_API.Classes.RequestModels {
    public class FileUploadRequest {

        public IFormFile file { get; set; }

        public string folderName { get; set; }
    }
}
