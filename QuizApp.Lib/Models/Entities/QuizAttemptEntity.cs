using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

public class QuizAttemptEntity
{
    [Key]
    public int AttemptID { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }
    public UserEntity User { get; set; } = null!;

    public DateTime AttemptDate { get; set; }
    public int TotalQuestionsAttempted { get; set; }
    public int CorrectAnswersCount { get; set; }
    public int IncorrectAnswersCount { get; set; }
}
