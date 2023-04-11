using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Migrations
{
    /// <inheritdoc />
    public partial class makeSavedQuestionInNewTableWithCreationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedQuestions_Questions_SavedQuestionsId",
                table: "SavedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedQuestions_Users_UsersWhoSaveThisQuestionId",
                table: "SavedQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions");

            migrationBuilder.RenameColumn(
                name: "UsersWhoSaveThisQuestionId",
                table: "SavedQuestions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SavedQuestionsId",
                table: "SavedQuestions",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedQuestions_UsersWhoSaveThisQuestionId",
                table: "SavedQuestions",
                newName: "IX_SavedQuestions_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SavedQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SavedQuestions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SavedQuestions_QuestionId",
                table: "SavedQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedQuestions_Questions_QuestionId",
                table: "SavedQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedQuestions_Users_UserId",
                table: "SavedQuestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedQuestions_Questions_QuestionId",
                table: "SavedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedQuestions_Users_UserId",
                table: "SavedQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SavedQuestions_QuestionId",
                table: "SavedQuestions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SavedQuestions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SavedQuestions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SavedQuestions",
                newName: "UsersWhoSaveThisQuestionId");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "SavedQuestions",
                newName: "SavedQuestionsId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedQuestions_UserId",
                table: "SavedQuestions",
                newName: "IX_SavedQuestions_UsersWhoSaveThisQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedQuestions",
                table: "SavedQuestions",
                columns: new[] { "SavedQuestionsId", "UsersWhoSaveThisQuestionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SavedQuestions_Questions_SavedQuestionsId",
                table: "SavedQuestions",
                column: "SavedQuestionsId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedQuestions_Users_UsersWhoSaveThisQuestionId",
                table: "SavedQuestions",
                column: "UsersWhoSaveThisQuestionId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
