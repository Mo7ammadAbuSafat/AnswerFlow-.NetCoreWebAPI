using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Entities;

namespace PersistenceLayer.DbContexts
{
    public class AnswerFlowContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ABUSAFAT;Initial Catalog= AnswerFlow;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                    .HasMany(t => t.Tags)
                    .WithMany(t => t.Questions);

            modelBuilder.Entity<User>()
                     .HasMany(t => t.FollowingUsers)
                     .WithMany(t => t.FollowerUsers)
                     .UsingEntity(j => j.ToTable("Following"));

            modelBuilder.Entity<Question>()
                    .HasOne<User>(s => s.User)
                    .WithMany(g => g.Questions)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SavedQuestion> SavedQuestions { get; set; }
        public DbSet<QuestionVote> QuestionVotes { get; set; }
        public DbSet<QuestionReport> QuestionReports { get; set; }
        public DbSet<QuestionHistory> QuestionHistories { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }
        public DbSet<AnswerReport> AnswerReports { get; set; }
        public DbSet<Replay> Replays { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
