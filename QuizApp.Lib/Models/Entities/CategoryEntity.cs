using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

public class CategoryEntity
{
    [Key]
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = null!;
}
