using QuizApp.Lib.Enums;
using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Models.Registrations;
using QuizApp.Lib.Repositories;
using System.Diagnostics;

namespace QuizApp.Lib.Services;

public interface IUserService
{
    User? CurrentUser { get; }
    Task<bool> RegisterNewUser(UserRegistration user);
    Task<bool> GetUser(UserRegistration user);
    Task<bool> UpdateUser(UserRegistration user);
    Task<IEnumerable<UserQuestionHistory>> GetAllUserQuestionHistory();
    Task<bool> ClearUserQuestionHistory();
    Task<IEnumerable<UserScoreEntity>> GetAllUserScores();
}
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserQuestionHistoryRepository questionHistoryRepository;
    private readonly IUserScoreRepository userScoreRepository;

    public UserService(IUserRepository userRepository, IUserQuestionHistoryRepository questionHistoryRepository, IUserScoreRepository userScoreRepository)
    {
        this.userRepository = userRepository;
        this.questionHistoryRepository = questionHistoryRepository;
        this.userScoreRepository = userScoreRepository;
    }

    // Currently logged in user
    public User? CurrentUser { get; private set; }

    // Register New User and set current user
    public async Task<bool> RegisterNewUser(UserRegistration user)
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
    public async Task<bool> GetUser(UserRegistration user)
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
    public async Task<bool> UpdateUser(UserRegistration user)
    {
        try
        {
            return await userRepository.UpdateAsync(user);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Get question history for current user
    public async Task<IEnumerable<UserQuestionHistory>> GetAllUserQuestionHistory()
    {
        try
        {
            if (CurrentUser == null || CurrentUser.UserRole is UserRole.Guest)
                return null!;

            return (await questionHistoryRepository.ReadRangeAsync(uq => uq.User.Username == CurrentUser.Username)).Select(entity => (UserQuestionHistory)entity);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // Clear question history for current user
    public async Task<bool> ClearUserQuestionHistory()
    {
        try
        {
            if (CurrentUser == null || CurrentUser.UserRole is UserRole.Guest)
                return false;

            return await questionHistoryRepository.DeleteRangeAsync(uq => uq.User.Username == CurrentUser.Username);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<UserScoreEntity>> GetAllUserScores()
    {
        try
        {
            return await userScoreRepository.ReadAllAsync() ?? null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
}
public interface IQuizService
{

}
public class QuizService : IQuizService
{
    public QuizService(IUserService userService)
    {

    }

    // Start Quiz - Difficulty/Category/Language

    // Save QuizAttempt
}
public interface IQuestionService
{

}
public class QuestionService : IQuestionService
{
    public QuestionService(UserService userService)
    {

    }

    // Create new Question with Answers

    // Get all questions created by user

    // Get Questions - Difficulty/Category/Language - To use in quiz
}
public interface IUserQuestionHistoryService
{

}
public class UserQuestionHistoryService : IUserQuestionHistoryService
{
    public UserQuestionHistoryService()
    {

    }

    // Addrange questionhistories

    // Clear History
}
