namespace OrderUp_API.Repository {
    public class TableRepository : AbstractRepository<Table> {

        public TableRepository(OrderUpDbContext context) : base(context) { }
    }
}
