namespace OrderUp_API.Classes.ResponseModels {
    public class GetPermissionsByAdminResponse {
        public string AdminName { get; set; }
        public Dictionary<string, List<Permission>> PermissionGroups { get; set; }
    }

}
