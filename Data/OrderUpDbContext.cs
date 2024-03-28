namespace OrderUp_API.Data {
    public class OrderUpDbContext : DbContext {

        public OrderUpDbContext(DbContextOptions<OrderUpDbContext> options) : base(options) {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseSnakeCaseNamingConvention();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            

            base.OnModelCreating(modelBuilder);
        }

        public void SeedPermissions(IServiceProvider services) {

        }

        public OrderUpDbContext() { }

        public DbSet<MenuCategory> MenuCategory { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuItemImage> MenuItemImages { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<SavedRestaurant> SavedRestaurants { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<SideItem> SideItems { get; set; }

        public DbSet<Sides> Sides { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<VerificationCode> VerificationCode { get; set; }


    }

}
