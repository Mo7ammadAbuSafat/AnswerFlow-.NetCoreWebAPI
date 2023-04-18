using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Migrations
{
    /// <inheritdoc />
    public partial class RenameSaversForQuestionAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedQuestions_Users_SaversForQuestionId",
                table: "SavedQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SavedQuestions_SaversForQuestionId",
                table: "SavedQuestions");

            migrationBuilder.RenameColumn(
                name: "SaversForQuestionId",
                table: "SavedQuestions",
                newName: "QuestionSaversId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions",
                columns: new[] { "QuestionSaversId", "SavedQuestionsId" });

            migrationBuilder.CreateIndex(
                name: "IX_SavedQuestions_SavedQuestionsId",
                table: "SavedQuestions",
                column: "SavedQuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedQuestions_Users_QuestionSaversId",
                table: "SavedQuestions",
                column: "QuestionSaversId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedQuestions_Users_QuestionSaversId",
                table: "SavedQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SavedQuestions_SavedQuestionsId",
                table: "SavedQuestions");

            migrationBuilder.RenameColumn(
                name: "QuestionSaversId",
                table: "SavedQuestions",
                newName: "SaversForQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions",
                columns: new[] { "SavedQuestionsId", "SaversForQuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_SavedQuestions_SaversForQuestionId",
                table: "SavedQuestions",
                column: "SaversForQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedQuestions_Users_SaversForQuestionId",
                table: "SavedQuestions",
                column: "SaversForQuestionId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
