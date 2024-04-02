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

        public async Task<bool> UpdateAdminPermissions(Guid adminId, List<int> permissionIds) {

            if(permissionIds.Count == 0) {
                return true;
            }

            try {
                // 1. Remove existing AdminPermissions for the given adminId
                await context.AdminPermissions.Where(ap => ap.AdminID == adminId).ExecuteDeleteAsync();

                // 2. Add the new mappings efficiently
                await context.AdminPermissions.AddRangeAsync(
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
