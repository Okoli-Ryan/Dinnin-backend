using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class adminpermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: true),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admin_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    adminid = table.Column<Guid>(name: "admin_id", type: "char(36)", nullable: false),
                    permissionid = table.Column<Guid>(name: "permission_id", type: "char(36)", nullable: false),
                    activestatus = table.Column<bool>(name: "active_status", type: "tinyint(1)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime(6)", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admin_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_admin_permissions_admin_admin_id",
                        column: x => x.adminid,
                        principalTable: "Admin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_admin_permissions_permissions_permission_id",
                        column: x => x.permissionid,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_admin_permissions_admin_id",
                table: "admin_permissions",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "ix_admin_permissions_permission_id",
                table: "admin_permissions",
                column: "permission_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_permissions");

            migrationBuilder.DropTable(
                name: "permissions");
        }
    }
}
