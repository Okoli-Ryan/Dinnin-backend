namespace OrderUp_API.Repository {
    public class AdminPermissionRepository {

        readonly OrderUpDbContext context;

        public AdminPermissionRepository(OrderUpDbContext context) {
            this.context = context;
        }

        public async Task<List<string>> GetPermissionNamesByAdminID(Guid adminId) {
            return await context.AdminPermissions
                .Where(x => x.AdminID.Equals(adminId))
                .Select(x => x.Permission.Name)
                .ToListAsync();
        }
        
        
        public async Task<Dictionary<string, List<Permission>>> GetPermissionsByAdminID(Guid adminId) {
            return await context.AdminPermissions
                .Where(x => x.AdminID.Equals(adminId))
                .Include(x => x.Permission)
                .GroupBy(x => x.Permission.Category)
                .ToDictionaryAsync(group => group.Key, group => group.Select(x => x.Permission)
                .ToList());
        }

        public async Task<bool> UpdateAdminPermissions(Guid adminId, List<int> permissionIds) {

            if(permissionIds.Count == 0) {
                return true;
            }

            try {

                var previousPermissions = context.AdminPermissions.Where(ap => ap.AdminID == adminId);

                context.RemoveRange(previousPermissions);

                context.AdminPermissions.AddRange(
                    permissionIds.Select(permissionId => new AdminPermission { AdminID = adminId, PermissionID = permissionId })
                );

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
