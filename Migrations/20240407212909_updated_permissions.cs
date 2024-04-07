using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedpermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 19);

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
                    { -20903238, "Can Create Menu", "MENU", "MENU__CREATE_MENU" },
                    { 150063632, "Can Create Table", "TABLE", "TABLE__CREATE_TABLE" },
                    { 424904238, "Can View user permissions", "PERMISSIONS", "PERMISSIONS__VIEW_PERMISSIONS" },
                    { 436955743, "Can Delete Menu Item", "MENU_ITEM", "MENU_ITEM__DELETE_MENU_ITEM" },
                    { 514957683, "Can View Analytics Order Item Count", "ANALYTICS", "ANALYTICS__ORDER_ITEM_COUNT" },
                    { 718467160, "Can View Orders", "ORDERS", "ORDERS__VIEW_ORDERS" },
                    { 889073934, "Can Update Restaurant", "RESTAURANT", "RESTAURANT__UPDATE_RESTAURANT" },
                    { 1451189193, "Can Update Table", "TABLE", "TABLE__UPDATE_TABLE" },
                    { 1508837528, "Can Update user permissions", "PERMISSIONS", "PERMISSIONS__UPDATE_PERMISSIONS" },
                    { 1543406304, "Can Delete Table", "TABLE", "TABLE__DELETE_TABLE" },
                    { 1786913524, "Can View Analytics Order Count", "ANALYTICS", "ANALYTICS__ORDER_COUNT" },
                    { 1929563487, "Can Update Menu Item", "MENU_ITEM", "MENU_ITEM__UPDATE_MENU_ITEM" },
                    { 1982228487, "Can Update Orders", "ORDERS", "ORDERS__UPDATE_ORDERS" },
                    { 2119304260, "Can Delete Staff", "STAFF", "STAFF__DELETE_STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 150063632);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 424904238);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 436955743);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 514957683);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 718467160);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 889073934);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1451189193);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1508837528);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1543406304);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1786913524);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1929563487);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1982228487);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 2119304260);

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "alias", "category", "name" },
                values: new object[,]
                {
                    { 1, "Can View Analytics Breakdown", "ANALYTICS", "ANALYTICS__BREAKDOWN" },
                    { 2, "Can View Analytics Order Amount", "ANALYTICS", "ANALYTICS__ORDER_AMOUNT" },
                    { 3, "Can View Analytics Order Count", "ANALYTICS", "ANALYTICS__ORDER_COUNT" },
                    { 4, "Can View Analytics Order Item Count", "ANALYTICS", "ANALYTICS__ORDER_ITEM_COUNT" },
                    { 5, "Can View Orders", "ORDERS", "ORDERS__VIEW_ORDERS" },
                    { 6, "Can Update Orders", "ORDERS", "ORDERS__UPDATE_ORDERS" },
                    { 7, "Can Update Menu", "MENU", "MENU__UPDATE_MENU" },
                    { 8, "Can Delete Menu", "MENU", "MENU__DELETE_MENU" },
                    { 9, "Can Create Menu", "MENU", "MENU__CREATE_MENU" },
                    { 10, "Can Create Menu Item", "MENU_ITEM", "MENU_ITEM__CREATE_MENU_ITEM" },
                    { 11, "Can Update Menu Item", "MENU_ITEM", "MENU_ITEM__UPDATE_MENU_ITEM" },
                    { 12, "Can Delete Menu Item", "MENU_ITEM", "MENU_ITEM__DELETE_MENU_ITEM" },
                    { 13, "Can Create Table", "TABLE", "TABLE__CREATE_TABLE" },
                    { 14, "Can Update Table", "TABLE", "TABLE__UPDATE_TABLE" },
                    { 15, "Can Delete Table", "TABLE", "TABLE__DELETE_TABLE" },
                    { 16, "Can Create Staff", "STAFF", "STAFF__CREATE_STAFF" },
                    { 17, "Can Update Staff", "STAFF", "STAFF__UPDATE_STAFF" },
                    { 18, "Can Delete Staff", "STAFF", "STAFF__DELETE_STAFF" },
                    { 19, "Can Update Restaurant", "RESTAURANT", "RESTAURANT__UPDATE_RESTAURANT" }
                });
        }
    }
}
