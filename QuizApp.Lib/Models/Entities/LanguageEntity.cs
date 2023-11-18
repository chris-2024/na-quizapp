using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

public class LanguageEntity
{
    [Key]
    public int LanguageID { get; set; }
    public string LanguageName { get; set; } = null!;
}
