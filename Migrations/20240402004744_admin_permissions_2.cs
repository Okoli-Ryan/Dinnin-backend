using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class adminpermissions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_permission",
                columns: table => new
                {
                    adminsid = table.Column<Guid>(name: "admins_id", type: "char(36)", nullable: false),
                    permissionsid = table.Column<int>(name: "permissions_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admin_permission", x => new { x.adminsid, x.permissionsid });
                    table.ForeignKey(
                        name: "fk_admin_permission_admin_admins_id",
                        column: x => x.adminsid,
                        principalTable: "Admin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_admin_permission_permissions_permissions_id",
                        column: x => x.permissionsid,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_admin_permission_permissions_id",
                table: "admin_permission",
                column: "permissions_id");
        }
    }
}
