using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace OrderUp_API.Utils {
    public class CloudinaryService : IImageUploadService {

        readonly Cloudinary cloudinary;

        public CloudinaryService() {

            var cloudName = ConfigurationUtil.GetConfigurationValue("Cloudinary:CLOUD_NAME");
            var apiKey = ConfigurationUtil.GetConfigurationValue("Cloudinary:API_KEY");
            var apiSecret = ConfigurationUtil.GetConfigurationValue("Cloudinary:API_SECRET");


            var CloudinaryAccount = new Account(cloudName, apiKey, apiSecret);

            cloudinary = new Cloudinary(CloudinaryAccount);
        }

        public DefaultResponse<string> Upload(IFormFile file, string FolderName) {

            if (file is null || file.Length == 0) return new DefaultErrorResponse<string> {
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

                return new DefaultSuccessResponse<string>(imageUrl);
            }

            catch (Exception e) {

                Debug.WriteLine(e.Message);
                return new DefaultErrorResponse<string> {
                    ResponseMessage = "Unable to upload image"
                };
            }
        }
    }
}
