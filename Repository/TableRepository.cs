namespace OrderUp_API.Repository {
    public class TableRepository : AbstractRepository<Table> {

        public TableRepository(OrderUpDbContext context) : base(context) { }

        public async Task<Table> GetTableData(Guid TableID) {
            return await context.Tables
                .Where(x => x.ID.Equals(TableID))
                .Include(x => x.Restaurant)
                .ThenInclude(x => x.MenuCategories)
                .ThenInclude(x => x.MenuItems)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
