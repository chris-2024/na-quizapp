using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using QuizApp.Lib.Models.Registrations;

namespace QuizApp.Lib.Models.Entities;

public class UserEntity
{
    [Key]
    public int UserID { get; set; }
    public string Username { get; set; } = null!;
    public string? Password { get; set; }

    [ForeignKey("UserRole")]
    public int UserRoleID { get; set; }
    public UserRoleEntity UserRole { get; set; } = null!;

    // Convert UserRegistration to UserEntity
    public static implicit operator UserEntity(UserRegistration user)
    {
        return new UserEntity()
        {
            Username = user.Username,
            Password = user.Password,
            UserRoleID = (int)user.UserRole
        };
    }
}