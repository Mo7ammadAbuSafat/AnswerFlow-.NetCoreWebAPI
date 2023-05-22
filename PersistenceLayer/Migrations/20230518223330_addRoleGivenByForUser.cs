using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Migrations
{
    /// <inheritdoc />
    public partial class addRoleGivenByForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleGivenByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleGivenByUserId",
                table: "Users",
                column: "RoleGivenByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_RoleGivenByUserId",
                table: "Users",
                column: "RoleGivenByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_RoleGivenByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleGivenByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleGivenByUserId",
                table: "Users");
        }
    }
}
