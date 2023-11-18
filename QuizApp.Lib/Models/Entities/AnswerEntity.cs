using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

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
