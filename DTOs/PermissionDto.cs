namespace OrderUp_API.DTOs {
    public class PermissionDto {

        public int id { get; set; }

        public string permissionName { get; set; }

        public string permissionDescription { get; set; }

        public string permissionCategory { get; set; }

        public List<AdminDto> admins { get; set; }

        public List<AdminPermissionDto> adminPermissions { get; set; }
    }
}
