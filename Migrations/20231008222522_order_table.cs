using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class ordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_order_table_id",
                table: "order",
                column: "table_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_tables_table_id",
                table: "order",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_tables_table_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "ix_order_table_id",
                table: "order");
        }
    }
}
