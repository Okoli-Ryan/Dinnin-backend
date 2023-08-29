using OrderUp_API.Classes.ResponseModels;

namespace OrderUp_API.Interfaces {
    public interface IImageUploadService {

        public DefaultResponse<FileUploadResponse> Upload(IFormFile file, string folderName);
    }
}
