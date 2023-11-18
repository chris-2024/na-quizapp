using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using QuizApp.Lib.Models.Registrations;

namespace QuizApp.Lib.Models.Entities;

// User
public class UserEntity
{
    [Key]
    public int UserID { get; set; }
    public string Username { get; set; } = null!;
    public string? Password { get; set; }

    [ForeignKey("UserRole")]
    public int UserRoleID { get; set; }
    public UserRoleEntity UserRole { get; set; } = null!;

    public static implicit operator UserEntity(UserRegistration user)
    {
        return new UserEntity()
        {
            Username = user.Username,
            Password = user.Password,
            UserRoleID = (int)user.UserRole
        };
    }
}

public class UserRoleEntity
{
    [Key]
    public int UserRoleID { get; set; }
    public string RoleName { get; set; } = null!;
}

// Quiz
public class UserQuestionHistoryEntity
{
    [ForeignKey("User")]
    public int UserID { get; set; }
    public UserEntity User { get; set; } = null!;

    [ForeignKey("Question")]
    public int QuestionID { get; set; }
    public QuestionEntity Question { get; set; } = null!;
    public DateTime AnsweredDate { get; set; }
}

public class QuizAttemptEntity
{
    [Key]
    public int AttemptID { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }
    public UserEntity User { get; set; } = null!;

    public DateTime AttemptDate { get; set; }
    public int Score { get; set; }
    public int TotalQuestionsAttempted { get; set; }
    public int CorrectAnswersCount { get; set; }
    public int IncorrectAnswersCount { get; set; }
}

public class UserScoreEntity
{
    [Key, ForeignKey("User")]
    public int UserID { get; set; }
    public UserEntity User { get; set; } = null!;
    public int CorrectAnswers { get; set; }
    public int IncorrectAnswers { get; set; }
    public DateTime LastScoredDate { get; set; }
}

// Question
public class QuestionEntity
{
    [Key]
    public int QuestionID { get; set; }

    [ForeignKey("Category")]
    public int CategoryID { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    [ForeignKey("Difficulty")]
    public int DifficultyID { get; set; }
    public DifficultyEntity Difficulty { get; set; } = null!;

    [ForeignKey("Language")]
    public int LanguageID { get; set; }
    public LanguageEntity Language { get; set; } = null!;

    public string QuestionText { get; set; } = null!;

    // Navigation property
    public ICollection<AnswerEntity>? Answers { get; set; }
}

public class AnswerEntity
{
    [Key]
    public int AnswerID { get; set; }

    [ForeignKey("Question")]
    public int QuestionID { get; set; }
    public QuestionEntity Question { get; set; } = null!;

    public string AnswerText { get; set; } = null!;
    public bool IsCorrect { get; set; }
}

public class CategoryEntity
{
    [Key]
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = null!;
}

public class DifficultyEntity
{
    [Key]
    public int DifficultyID { get; set; }
    public string DifficultyName { get; set; } = null!;
}

public class LanguageEntity
{
    [Key]
    public int LanguageID { get; set; }
    public string LanguageName { get; set; } = null!;
}
