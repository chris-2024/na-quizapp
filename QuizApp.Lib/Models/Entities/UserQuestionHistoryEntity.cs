using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Lib.Models.Entities;

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