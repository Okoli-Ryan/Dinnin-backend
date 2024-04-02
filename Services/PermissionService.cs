namespace OrderUp_API.Services {
    public class PermissionService {

        readonly PermissionRepository permissionRepository;

        public PermissionService(PermissionRepository permissionRepository) {
            this.permissionRepository = permissionRepository;

        }

        public async Task<DefaultResponse<Dictionary<string, List<Permission>>>> GetPermissions() {

            var response = await permissionRepository.GetPermissions();

            if (response is null) {
                return new DefaultErrorResponse<Dictionary<string, List<Permission>>>();
            }

            return new DefaultSuccessResponse<Dictionary<string, List<Permission>>>(response);

        }
    }
}
