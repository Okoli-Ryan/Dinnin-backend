using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using OrderUp_API.Classes.ResponseModels;

namespace OrderUp_API.Utils {
    public class CloudinaryService : IImageUploadService {

        readonly Cloudinary cloudinary;

        public CloudinaryService() {

            var cloudName = ConfigurationUtil.GetConfigurationValue("Cloudinary_CLOUD_NAME");
            var apiKey = ConfigurationUtil.GetConfigurationValue("Cloudinary_API_KEY");
            var apiSecret = ConfigurationUtil.GetConfigurationValue("Cloudinary_API_SECRET");


            var CloudinaryAccount = new Account(cloudName, apiKey, apiSecret);

            cloudinary = new Cloudinary(CloudinaryAccount);
        }

        public DefaultResponse<FileUploadResponse> Upload(IFormFile file, string FolderName) {

            if (file is null || file.Length == 0) return new DefaultErrorResponse<FileUploadResponse> {
                ResponseMessage = "No file was uploaded"
            };

            try {


                var uploadParams = new ImageUploadParams {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = FolderName,
                    PublicId = RandomStringGenerator.GenerateRandomString(20),
                    Transformation = new Transformation().Gravity("auto").Height(600).Width(600).Crop("fill").Radius("max")
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                var imageUrl = uploadResult.SecureUrl.ToString();

                FileUploadResponse response = new FileUploadResponse() { url = imageUrl };

                return new DefaultSuccessResponse<FileUploadResponse>(response);
            }

            catch (Exception e) {

                Debug.WriteLine(e.Message);
                return new DefaultErrorResponse<FileUploadResponse> {
                    ResponseMessage = "Unable to upload image"
                };
            }
        }
    }
}
