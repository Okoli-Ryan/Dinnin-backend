namespace OrderUp_API.Models {
    [Microsoft.EntityFrameworkCore.Index(nameof(Name), IsUnique = true)]
    public class Permission {

        public Permission() { }

        public Permission(PermissionName name, string alias) {
            Name = name.ToString();
            Category = name.ToString().Split("__")[0];
            Alias = alias;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]

        public string Category { get; set; }

        [MaxLength(100)]
        public string Alias { get; set; }

        public List<Admin> Admins { get; set; } = new ();

        public List<AdminPermission> AdminPermissions { get; set; } = new ();
    }
}
