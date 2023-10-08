namespace OrderUp_API.Repository {
    public class TableRepository : AbstractRepository<Table> {

        public TableRepository(OrderUpDbContext context) : base(context) { }

        public async Task<Table> GetTableData(string TableCode) {
            return await context.Tables
                .Where(x => x.Code.Equals(TableCode))
                .Include(x => x.Restaurant)
                .ThenInclude(x => x.MenuCategories.Where(x => x.ActiveStatus).OrderBy(x => x.Order))
                .ThenInclude(x => x.MenuItems)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Table>> GetTablesByRestaurantID(Guid RestaurantID) {
            return await context.Tables.Where(x => x.RestaurantID.Equals(RestaurantID)).ToListAsync();
        }

    }
}
