using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class orderitemmenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "menu_item_name",
                table: "order_item",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "order_item_price",
                table: "order_item",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "menu_item_name",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "order_item_price",
                table: "order_item");
        }
    }
}
