using QuizApp.Lib.Enums;
using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Models.Registrations;
using QuizApp.Lib.Repositories;
using System.Diagnostics;

namespace QuizApp.Lib.Services;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserQuestionHistoryRepository questionHistoryRepository;
    private readonly IUserScoreRepository userScoreRepository;
    private readonly IQuizAttemptRepository quizAttemptRepository;

    public UserService(IUserRepository userRepository, IUserQuestionHistoryRepository questionHistoryRepository, IUserScoreRepository userScoreRepository, IQuizAttemptRepository quizAttemptRepository)
    {
        this.userRepository = userRepository;
        this.questionHistoryRepository = questionHistoryRepository;
        this.userScoreRepository = userScoreRepository;
        this.quizAttemptRepository = quizAttemptRepository;
    }

    // Currently logged in user
    public User? CurrentUser { get; set; }

    // True if CurrentUser is registered
    public bool IsRegistered => (CurrentUser != null && CurrentUser.UserRole is UserRole.Registered);

    // Register New User and set current user
    public async Task<bool> RegisterNewUserAsync(UserRegistration user)
    {
        try
        {
            var entity = await userRepository.CreateAsync(user);
            if (entity != null) CurrentUser = entity;
            return CurrentUser != null;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Set current user
    public async Task<bool> GetUserAsync(UserRegistration user)
    {
        try
        {
            var entity = await userRepository.ReadAsync(u => u.Username == user.Username && u.Password == user.Password);
            if (entity != null) CurrentUser = entity;
            return CurrentUser != null;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Update User
    public async Task<(bool, string)> UpdateUserAsync(UserRegistration user)
    {
        try
        {
            if (!IsRegistered)
                return (false, "User not registered");

            var existingUser = await userRepository.ReadAsync(u => u.Username == user.Username && u.UserID != CurrentUser!.Id);

            if (existingUser != null)
                return (false, "Username already exist");

            var entity = await userRepository.ReadAsync(u => u.UserID == CurrentUser!.Id);

            entity.Username = user.Username;
            entity.Password = user.Password;

            var updatedEntity = await userRepository.UpdateAsync(entity);

            if (updatedEntity != null) CurrentUser = updatedEntity;
            return (CurrentUser != null, "Updated");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return (false, "Failed to update");
    }

    // Remove User
    public async Task<bool> RemoveUserAsync()
    {
        try
        {
            if (!IsRegistered)
                return false;

            var user = await userRepository.ReadAsync(u => u.UserID == CurrentUser!.Id);

            var result = await userRepository.DeleteAsync(user);

            if (result) CurrentUser = null;

            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Get question history for current user
    public async Task<IEnumerable<UserQuestionHistory>> GetAllUserQuestionHistoryAsync()
    {
        try
        {
            if (!IsRegistered)
                return null!;

            var questions = (await questionHistoryRepository.ReadRangeAsync(uq => uq.UserID == CurrentUser!.Id)).Select(entity => (UserQuestionHistory)entity);
            return questions;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // Clear question history for current user
    public async Task<bool> ClearUserQuestionHistoryAsync()
    {
        try
        {
            if (!IsRegistered)
                return false;

            var result = await questionHistoryRepository.DeleteRangeAsync(uq => uq.UserID == CurrentUser!.Id);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}