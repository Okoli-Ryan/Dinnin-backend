namespace OrderUp_API.Models {
    public class Permission : AbstractEntity {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Admin> Admins { get; set; } = new ();

        public List<AdminPermission> AdminPermissions { get; set; } = new ();
    }
}
