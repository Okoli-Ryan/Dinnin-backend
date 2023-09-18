using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeletemenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_items_menu_category_menu_category_id",
                table: "menu_items");

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                table: "verification_code",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "menu_category_id",
                table: "menu_items",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "Admin",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_items_menu_category_menu_category_id",
                table: "menu_items",
                column: "menu_category_id",
                principalTable: "menu_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_items_menu_category_menu_category_id",
                table: "menu_items");

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                table: "verification_code",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<Guid>(
                name: "menu_category_id",
                table: "menu_items",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "Admin",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_items_menu_category_menu_category_id",
                table: "menu_items",
                column: "menu_category_id",
                principalTable: "menu_category",
                principalColumn: "id");
        }
    }
}
