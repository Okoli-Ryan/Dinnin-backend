using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class positivepermissionids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -2133081567);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -1414419149);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -891860850);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -506956829);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -505040706);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -395124802);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -300151959);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -88353422);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: -20903238);

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "alias", "category", "name" },
                values: new object[,]
                {
                    { 20903238, "Can Create Menu", "MENU", "MENU__CREATE_MENU" },
                    { 88353422, "Can Create Staff", "STAFF", "STAFF__CREATE_STAFF" },
                    { 300151959, "Can Update Menu", "MENU", "MENU__UPDATE_MENU" },
                    { 395124802, "Can Delete Menu", "MENU", "MENU__DELETE_MENU" },
                    { 505040706, "Can View Analytics Breakdown", "ANALYTICS", "ANALYTICS__BREAKDOWN" },
                    { 506956829, "Can View Analytics Order Amount", "ANALYTICS", "ANALYTICS__ORDER_AMOUNT" },
                    { 891860850, "Can View Staff", "STAFF", "STAFF__VIEW_STAFF" },
                    { 1414419149, "Can Update Staff", "STAFF", "STAFF__UPDATE_STAFF" },
                    { 2133081567, "Can Create Menu Item", "MENU_ITEM", "MENU_ITEM__CREATE_MENU_ITEM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 20903238);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 88353422);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 300151959);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 395124802);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 505040706);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 506956829);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 891860850);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1414419149);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 2133081567);

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "alias", "category", "name" },
                values: new object[,]
                {
                    { -2133081567, "Can Create Menu Item", "MENU_ITEM", "MENU_ITEM__CREATE_MENU_ITEM" },
                    { -1414419149, "Can Update Staff", "STAFF", "STAFF__UPDATE_STAFF" },
                    { -891860850, "Can View Staff", "STAFF", "STAFF__VIEW_STAFF" },
                    { -506956829, "Can View Analytics Order Amount", "ANALYTICS", "ANALYTICS__ORDER_AMOUNT" },
                    { -505040706, "Can View Analytics Breakdown", "ANALYTICS", "ANALYTICS__BREAKDOWN" },
                    { -395124802, "Can Delete Menu", "MENU", "MENU__DELETE_MENU" },
                    { -300151959, "Can Update Menu", "MENU", "MENU__UPDATE_MENU" },
                    { -88353422, "Can Create Staff", "STAFF", "STAFF__CREATE_STAFF" },
                    { -20903238, "Can Create Menu", "MENU", "MENU__CREATE_MENU" }
                });
        }
    }
}
