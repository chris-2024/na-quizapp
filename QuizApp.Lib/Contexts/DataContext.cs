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

        modelBuilder.Entity<UserQuestionHistoryEntity>()
            .HasOne(uqh => uqh.Question)
            .WithMany()
            .HasForeignKey(uqh => uqh.QuestionID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserQuestionHistoryEntity>()
            .HasOne(uq => uq.User)
            .WithMany()
            .HasForeignKey(uq => uq.UserID);

        // Seed UserRoles
        modelBuilder.Entity<UserRoleEntity>().HasData(
            new UserRoleEntity { UserRoleID = 1, RoleName = "Registered" },
            new UserRoleEntity { UserRoleID = 2, RoleName = "Guest" }
        );

        // Seed Categories
        modelBuilder.Entity<CategoryEntity>().HasData(
            new CategoryEntity { CategoryID = 1, CategoryName = "C#" },
            new CategoryEntity { CategoryID = 2, CategoryName = "Fruits" },
            new CategoryEntity { CategoryID = 3, CategoryName = "Potatoes" },
            new CategoryEntity { CategoryID = 4, CategoryName = "Technology" },
            new CategoryEntity { CategoryID = 5, CategoryName = "Other" }
        );

        // Seed Difficulties
        modelBuilder.Entity<DifficultyEntity>().HasData(
            new DifficultyEntity { DifficultyID = 1, DifficultyName = "Normal" },
            new DifficultyEntity { DifficultyID = 2, DifficultyName = "Hard" }
        );

        // Seed Languages
        modelBuilder.Entity<LanguageEntity>().HasData(
            new LanguageEntity { LanguageID = 1, LanguageName = "English" },
            new LanguageEntity { LanguageID = 2, LanguageName = "Swedish" }
        );
    }
}
