using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Server.Migrations
{
    public partial class bool_isAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserAccounts");

            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "UserAccounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "UserAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserAccounts",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
