using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public UserRole UserRole { get; set; }

    // Convert UserEntity to User
    public static implicit operator User(UserEntity user)
    {
        return new User()
        {
            Id = user.UserID,
            Username = user.Username,
            UserRole = (UserRole)user.UserRoleID
        };
    }
}