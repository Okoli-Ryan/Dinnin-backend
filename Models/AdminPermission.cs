namespace OrderUp_API.Models {
    public class AdminPermission : AbstractEntity {

        public Guid AdminID { get; set; }
        public int PermissionID { get; set; }
        public Permission Permission { get; set; }
        public Admin Admin { get; set; }

    }
}
