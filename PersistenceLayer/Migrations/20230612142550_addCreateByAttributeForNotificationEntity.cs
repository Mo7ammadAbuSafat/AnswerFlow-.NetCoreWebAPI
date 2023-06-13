using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Migrations
{
    /// <inheritdoc />
    public partial class addCreateByAttributeForNotificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedByUserId",
                table: "Notifications",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_CreatedByUserId",
                table: "Notifications",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_CreatedByUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CreatedByUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Notifications");
        }
    }
}
