namespace OrderUp_API.Repository {
    public class PermissionRepository {

        readonly OrderUpDbContext context;

        public PermissionRepository(OrderUpDbContext context) { 
            this.context = context;
        }

        public async Task<Dictionary<string, List<Permission>>> GetPermissions() { 
            return await context.Permissions
                                .GroupBy(x => x.Category)
                                .ToDictionaryAsync(group => group.Key, group => group.ToList());
        }

    }
}
