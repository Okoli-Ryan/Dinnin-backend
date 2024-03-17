using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class adminrecoveryEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "recovery_email",
                table: "Admin",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recovery_email",
                table: "Admin");
        }
    }
}
