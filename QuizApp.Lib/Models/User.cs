using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Models;

public class User
{
    public string Username { get; set; } = null!;
    public UserRole UserRole { get; set; }

    // Convert UserEntity to User
    public static implicit operator User(UserEntity user)
    {
        return new User()
        {
            Username = user.Username,
            UserRole = (UserRole)user.UserRoleID
        };
    }
}