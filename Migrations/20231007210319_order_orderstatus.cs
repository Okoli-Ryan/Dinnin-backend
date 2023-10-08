using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class orderorderstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "order_status",
                table: "order",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_status",
                table: "order");
        }
    }
}
