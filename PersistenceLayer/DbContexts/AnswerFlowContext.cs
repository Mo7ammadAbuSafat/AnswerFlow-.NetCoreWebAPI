using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;

namespace PersistenceLayer.DbContexts
{
    public class AnswerFlowContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionHistory> QuestionHistories { get; set; }
        public DbSet<QuestionReport> QuestionReports { get; set; }
        public DbSet<QuestionVote> QuestionVotes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerReport> AnswerReports { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ActivityDateView> ActivityDateView { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public AnswerFlowContext(DbContextOptions<AnswerFlowContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                     .HasMany(t => t.FollowingUsers)
                     .WithMany(t => t.FollowerUsers)
                     .UsingEntity(j => j.ToTable("Following"));

            modelBuilder.Entity<User>()
                    .HasMany(t => t.Tags)
                    .WithMany(t => t.Users)
                    .UsingEntity(j => j.ToTable("UserTag"));

            modelBuilder.Entity<User>()
                    .HasOne(s => s.RoleGivenByUser)
                    .WithMany(g => g.UsersThatGivenRoleByThisUser)
                    .HasForeignKey(s => s.RoleGivenByUserId);

            modelBuilder.Entity<Question>()
                    .HasMany(t => t.Tags)
                    .WithMany(t => t.Questions)
                    .UsingEntity(j => j.ToTable("QuestionTag"));

            modelBuilder.Entity<Question>()
                    .HasMany(t => t.QuestionSavers)
                    .WithMany(t => t.SavedQuestions)
                    .UsingEntity(j => j.ToTable("SavedQuestions"));

            modelBuilder.Entity<Question>()
                    .HasOne(s => s.User)
                    .WithMany(g => g.Questions)
                    .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<QuestionHistory>()
                    .HasOne(s => s.Question)
                    .WithMany(g => g.EditHistory)
                    .HasForeignKey(s => s.QuestionId);

            modelBuilder.Entity<Question>()
                    .HasOne(s => s.User)
                    .WithMany(g => g.Questions)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                   .HasOne(s => s.User)
                   .WithMany(g => g.Answers)
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionReport>()
                    .HasOne(s => s.Question)
                    .WithMany(g => g.Reports)
                    .HasForeignKey(s => s.QuestionId);

            modelBuilder.Entity<QuestionReport>()
                    .HasOne(s => s.User)
                    .WithMany(g => g.QuestionReports)
                    .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<AnswerReport>()
                    .HasOne(s => s.Answer)
                    .WithMany(g => g.Reports)
                    .HasForeignKey(s => s.AnswerId);

            modelBuilder.Entity<AnswerReport>()
                    .HasOne(s => s.Question)
                    .WithMany(g => g.AnswerReports)
                    .HasForeignKey(s => s.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnswerReport>()
                    .HasOne(s => s.User)
                    .WithMany(g => g.AnswerReports)
                    .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Keyword>()
                    .HasOne(s => s.Question)
                    .WithMany(g => g.Keywords)
                    .HasForeignKey(s => s.QuestionId);

            modelBuilder.Entity<Keyword>()
                    .HasIndex(e => e.name);

            modelBuilder.Entity<Notification>()
                    .HasOne(s => s.Question)
                    .WithMany(g => g.Notifications)
                    .HasForeignKey(s => s.QuestionId);

            modelBuilder.Entity<Notification>()
                   .HasOne(s => s.User)
                   .WithMany(g => g.Notifications)
                   .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Notification>()
                   .HasOne(s => s.CreatedByUser)
                   .WithMany(g => g.CreatedNotifications)
                   .HasForeignKey(s => s.CreatedByUserId);

            modelBuilder.Entity<User>().Property(u => u.About).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.VerifiedDate).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.ResetPasswordCode).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.ResetPasswordCodeExpiresDate).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.ImageId).IsRequired(false);
            modelBuilder.Entity<User>().Property(p => p.Type).HasDefaultValue(UserType.NormalUser);
            modelBuilder.Entity<User>().Property(u => u.Username).HasColumnType("varchar(40)");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnType("varchar(100)");
            modelBuilder.Entity<User>().Property(u => u.VerificationCode).HasColumnType("varchar(20)");
            modelBuilder.Entity<User>().Property(u => u.ResetPasswordCode).HasColumnType("varchar(20)");
            modelBuilder.Entity<User>().Property(u => u.About).HasColumnType("varchar(1000)");


            modelBuilder.Entity<Tag>().Property(t => t.SourceLink).IsRequired(false);

            modelBuilder.Entity<AnswerReport>().Property(p => p.Status).HasDefaultValue(ReportStatus.Open);
            modelBuilder.Entity<QuestionReport>().Property(p => p.Status).HasDefaultValue(ReportStatus.Open);

            modelBuilder.Entity<ActivityDateView>().HasNoKey().ToView("ActivityDateView");

        }
    }
}
