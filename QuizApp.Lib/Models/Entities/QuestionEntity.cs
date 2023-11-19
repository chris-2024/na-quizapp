using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using QuizApp.Lib.Enums;

namespace QuizApp.Lib.Models.Entities;

public class QuestionEntity
{
    [Key]
    public int QuestionID { get; set; }
    public string QuestionText { get; set; } = null!;

    [ForeignKey("Category")]
    public int CategoryID { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    [ForeignKey("Difficulty")]
    public int DifficultyID { get; set; }
    public DifficultyEntity Difficulty { get; set; } = null!;

    [ForeignKey("Language")]
    public int LanguageID { get; set; }
    public LanguageEntity Language { get; set; } = null!;

    [ForeignKey("User")]
    public int UserID { get; set; }
    public UserEntity User { get; set; } = null!;

    // Navigation property
    public ICollection<AnswerEntity>? Answers { get; set; }

    // Convert QuestionEntity to Question
    public static implicit operator QuestionEntity(Question question)
    {
        return new QuestionEntity()
        {
            QuestionText = question.QuestionText,
            CategoryID = question.Category.CategoryID,
            DifficultyID = (int)question.Difficulty,
            LanguageID = (int)question.Language,
            UserID = question.UserID,
            Answers = new List<AnswerEntity>(question.Answers!)
        };
    }
}
