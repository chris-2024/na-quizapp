using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

public class DifficultyEntity
{
    [Key]
    public int DifficultyID { get; set; }
    public string DifficultyName { get; set; } = null!;
}
