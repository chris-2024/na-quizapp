using System.ComponentModel.DataAnnotations;

namespace QuizApp.Lib.Models.Entities;

public class UserRoleEntity
{
    [Key]
    public int UserRoleID { get; set; }
    public string RoleName { get; set; } = null!;
}
