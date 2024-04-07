namespace OrderUp_API.Classes.ResponseDtos {
    public class GetPermissionsByAdminDto {

        public string adminName {  get; set; }

        public Dictionary<string, List<PermissionDto>> permissionGroups { get; set; }
    }
}
