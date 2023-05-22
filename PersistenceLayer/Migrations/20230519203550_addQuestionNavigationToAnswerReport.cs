using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Migrations
{
    /// <inheritdoc />
    public partial class addQuestionNavigationToAnswerReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "AnswerReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerReports_QuestionId",
                table: "AnswerReports",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerReports_Questions_QuestionId",
                table: "AnswerReports",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerReports_Questions_QuestionId",
                table: "AnswerReports");

            migrationBuilder.DropIndex(
                name: "IX_AnswerReports_QuestionId",
                table: "AnswerReports");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "AnswerReports");
        }
    }
}
