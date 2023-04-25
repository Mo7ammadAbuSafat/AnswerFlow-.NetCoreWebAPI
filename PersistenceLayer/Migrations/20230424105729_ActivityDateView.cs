using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Migrations
{
    /// <inheritdoc />
    public partial class ActivityDateView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW ActivityDateView AS
(
(select CreationDate as Date,UserId from dbo.Questions)
union 
(select CreationDate as Date,UserId from dbo.Answers)
union 
(select CreationDate as Date,UserId from dbo.AnswerReports)
union 
(select CreationDate as Date,UserId from dbo.QuestionReports)
union 
(select CreationDate as Date,UserId from dbo.QuestionVotes)
union 
(select CreationDate as Date,UserId from dbo.AnswerVotes));"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" DROP VIEW IF EXISTS ActivityDateView;");

        }
    }
}
