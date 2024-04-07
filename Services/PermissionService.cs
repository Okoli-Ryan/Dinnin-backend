namespace OrderUp_API.Services {
    public class PermissionService {

        readonly PermissionRepository permissionRepository;
        readonly IMapper mapper;

        public PermissionService(PermissionRepository permissionRepository, IMapper mapper) {
            this.permissionRepository = permissionRepository;
            this.mapper = mapper;
        }

        public async Task<DefaultResponse<Dictionary<string, List<PermissionDto>>>> GetPermissions() {

            var permissions = await permissionRepository.GetPermissions();

            if (permissions is null) {
                return new DefaultErrorResponse<Dictionary<string, List<PermissionDto>>>();
            }

            var mappedPermissions = mapper.Map<Dictionary<string, List<PermissionDto>>>(permissions);

            return new DefaultSuccessResponse<Dictionary<string, List<PermissionDto>>>(mappedPermissions);

        }
    }
}
