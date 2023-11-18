using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

public class UserScoreEntity
{
    [Key, ForeignKey("User")]
    public int UserID { get; set; }
    public UserEntity User { get; set; } = null!;
    public int CorrectAnswers { get; set; }
    public int IncorrectAnswers { get; set; }
    public DateTime LastScoredDate { get; set; }
}
