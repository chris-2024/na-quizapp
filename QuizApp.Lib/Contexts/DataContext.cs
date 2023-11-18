using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<AnswerEntity> Answers { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<UserScoreEntity> UserScores { get; set; }
    public DbSet<UserQuestionHistoryEntity> UserQuestionHistories { get; set; }
    public DbSet<DifficultyEntity> Difficulties { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<QuizAttemptEntity> QuizAttempts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Make Usernames unique
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Composite key: UserId and QuestionId
        modelBuilder.Entity<UserQuestionHistoryEntity>()
            .HasKey(uq => new { uq.UserID, uq.QuestionID });
    }
}
