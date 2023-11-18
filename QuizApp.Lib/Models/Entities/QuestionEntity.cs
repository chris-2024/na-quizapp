using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public int? UserID { get; set; }
    public UserEntity? User { get; set; }

    // Navigation property
    public ICollection<AnswerEntity>? Answers { get; set; }
}
