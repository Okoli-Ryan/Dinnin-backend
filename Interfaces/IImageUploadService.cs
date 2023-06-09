namespace OrderUp_API.Interfaces {
    public interface IImageUploadService {

        public DefaultResponse<string> Upload(IFormFile file, string folderName);
    }
}
