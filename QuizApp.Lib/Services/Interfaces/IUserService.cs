using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Models.Registrations;

namespace QuizApp.Lib.Services;

public interface IUserService
{
    User? CurrentUser { get; set; }
    bool IsRegistered { get; }
    Task<bool> RegisterNewUserAsync(UserRegistration user);
    Task<bool> GetUserAsync(UserRegistration user);
    Task<(bool Result, string Message)> UpdateUserAsync(UserRegistration user);
    Task<bool> RemoveUserAsync();
    Task<IEnumerable<UserQuestionHistory>> GetAllUserQuestionHistoryAsync();
    Task<bool> ClearUserQuestionHistoryAsync();
}
