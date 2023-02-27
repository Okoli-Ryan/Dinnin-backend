using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class mysql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ordernote = table.Column<string>(name: "order_note", type: "varchar(100)", maxLength: 100, nullable: true),
                    orderamount = table.Column<decimal>(name: "order_amount", type: "decimal(18,2)", nullable: false),
                    restaurantid = table.Column<Guid>(name: "restaurant_id", type: "char(36)", nullable: false),
                    paymentoption = table.Column<string>(name: "payment_option", type: "varchar(20)", maxLength: 20, nullable: true),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: true),
                    tableid = table.Column<Guid>(name: "table_id", type: "char(36)", nullable: true),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    slug = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    xcoordinate = table.Column<decimal>(name: "x_coordinate", type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    ycoordinate = table.Column<decimal>(name: "y_coordinate", type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    contactphonenumber = table.Column<string>(name: "contact_phone_number", type: "varchar(20)", maxLength: 20, nullable: false),
                    logourl = table.Column<string>(name: "logo_url", type: "varchar(500)", maxLength: 500, nullable: true),
                    contactemailaddress = table.Column<string>(name: "contact_email_address", type: "varchar(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurants", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "varchar(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "varchar(50)", maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "varchar(50)", maxLength: 50, nullable: false),
                    userimageurl = table.Column<string>(name: "user_image_url", type: "varchar(500)", maxLength: 500, nullable: true),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false),
                    password = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    isemailconfirmed = table.Column<bool>(name: "is_email_confirmed", type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "verification_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    userid = table.Column<Guid>(name: "user_id", type: "char(100)", maxLength: 100, nullable: false),
                    expirydate = table.Column<DateTime>(name: "expiry_date", type: "datetime(6)", nullable: false),
                    usertype = table.Column<string>(name: "user_type", type: "varchar(10)", maxLength: 10, nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_verification_code", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    restaurantid = table.Column<Guid>(name: "restaurant_id", type: "char(36)", nullable: false),
                    role = table.Column<string>(type: "longtext", nullable: true),
                    firstname = table.Column<string>(name: "first_name", type: "varchar(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "varchar(50)", maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "varchar(50)", maxLength: 50, nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false),
                    password = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    isemailconfirmed = table.Column<bool>(name: "is_email_confirmed", type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admin", x => x.id);
                    table.ForeignKey(
                        name: "fk_admin_restaurants_restaurant_id",
                        column: x => x.restaurantid,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    categoryname = table.Column<string>(name: "category_name", type: "varchar(50)", maxLength: 50, nullable: false),
                    restaurantid = table.Column<Guid>(name: "restaurant_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_category_restaurants_restaurant_id",
                        column: x => x.restaurantid,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    tablename = table.Column<string>(name: "table_name", type: "varchar(50)", maxLength: 50, nullable: true),
                    restaurantid = table.Column<Guid>(name: "restaurant_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tables", x => x.id);
                    table.ForeignKey(
                        name: "fk_tables_restaurants_restaurant_id",
                        column: x => x.restaurantid,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "saved_restaurants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    restaurantid = table.Column<Guid>(name: "restaurant_id", type: "char(36)", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_saved_restaurants", x => x.id);
                    table.ForeignKey(
                        name: "fk_saved_restaurants_restaurants_restaurant_id",
                        column: x => x.restaurantid,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_saved_restaurants_users_user_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    menuitemname = table.Column<string>(name: "menu_item_name", type: "varchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    restaurantid = table.Column<Guid>(name: "restaurant_id", type: "char(36)", nullable: false),
                    description = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    imageurl = table.Column<string>(name: "image_url", type: "varchar(500)", maxLength: 500, nullable: true),
                    menucategoryid = table.Column<Guid>(name: "menu_category_id", type: "char(36)", nullable: true),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_items_menu_category_menu_category_id",
                        column: x => x.menucategoryid,
                        principalTable: "menu_category",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu_item_images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    imageurl = table.Column<string>(name: "image_url", type: "varchar(500)", maxLength: 500, nullable: false),
                    menuitemid = table.Column<Guid>(name: "menu_item_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_item_images_menu_items_menu_item_id",
                        column: x => x.menuitemid,
                        principalTable: "menu_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    menuitemid = table.Column<Guid>(name: "menu_item_id", type: "char(36)", nullable: false),
                    orderid = table.Column<Guid>(name: "order_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_menu_items_menu_item_id",
                        column: x => x.menuitemid,
                        principalTable: "menu_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_item_order_order_id",
                        column: x => x.orderid,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sides",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    menuitemid = table.Column<Guid>(name: "menu_item_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sides", x => x.id);
                    table.ForeignKey(
                        name: "fk_sides_menu_items_menu_item_id",
                        column: x => x.menuitemid,
                        principalTable: "menu_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "side_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    sideitemname = table.Column<string>(name: "side_item_name", type: "varchar(100)", maxLength: 100, nullable: false),
                    sideitemprice = table.Column<decimal>(name: "side_item_price", type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sidesid = table.Column<Guid>(name: "sides_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_side_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_side_items_sides_sides_id",
                        column: x => x.sidesid,
                        principalTable: "sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_admin_restaurant_id",
                table: "Admin",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_category_restaurant_id",
                table: "menu_category",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_images_menu_item_id",
                table: "menu_item_images",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_items_menu_category_id",
                table: "menu_items",
                column: "menu_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_menu_item_id",
                table: "order_item",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_restaurants_slug",
                table: "restaurants",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_saved_restaurants_restaurant_id",
                table: "saved_restaurants",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_saved_restaurants_user_id",
                table: "saved_restaurants",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_side_items_sides_id",
                table: "side_items",
                column: "sides_id");

            migrationBuilder.CreateIndex(
                name: "ix_sides_menu_item_id",
                table: "sides",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_tables_restaurant_id",
                table: "tables",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "menu_item_images");

            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "saved_restaurants");

            migrationBuilder.DropTable(
                name: "side_items");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "verification_code");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "sides");

            migrationBuilder.DropTable(
                name: "menu_items");

            migrationBuilder.DropTable(
                name: "menu_category");

            migrationBuilder.DropTable(
                name: "restaurants");
        }
    }
}
