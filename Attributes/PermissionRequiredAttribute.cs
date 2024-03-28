namespace OrderUp_API.Attributes {
    public class PermissionRequiredAttribute : Attribute {
        public PermissionName PermissionName { get; }

        public PermissionRequiredAttribute(PermissionName permissionName) {
            PermissionName = permissionName;
        }
    }
}
