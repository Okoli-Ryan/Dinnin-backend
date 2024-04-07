namespace OrderUp_API.DTOs {
    public class AdminPermissionDto : AbstractDto {

        public int adminId { get; set; }

        public int permissionId { get; set; }

        public PermissionDto permission { get; set; }

        public AdminDto admin { get; set; }

    }
}
