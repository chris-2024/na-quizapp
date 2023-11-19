using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Models.Registrations;
using QuizApp.Lib.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace QuizApp.Lib.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository questionRepository;
    private readonly IAnswerRepository answerRepository;
    private readonly IUserQuestionHistoryRepository userQuestionHistoryRepository;

    public QuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository, IUserQuestionHistoryRepository userQuestionHistoryRepository)
    {
        this.questionRepository = questionRepository;
        this.answerRepository = answerRepository;
        this.userQuestionHistoryRepository = userQuestionHistoryRepository;
    }

    // Create new Question with Answers
    public async Task<bool> CreateQuestionAsync(Question question)
    {
        try
        {
            if (question.Answers == null || !question.Answers.Any()) 
                return false;

            // Creates Question along with Answers through ICollection<AnswerEntity> Answers in QuestionEntity
            var entity = await questionRepository.CreateAsync((QuestionEntity)question);

            return entity != null;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Create new Question with Answers
    public async Task<bool> AddQuestionHistoryAsync(Question question)
    {
        try
        {
            if (question.UserID <= 0) 
                return false;

            var entity = await userQuestionHistoryRepository.CreateAsync(question);

            return entity != null;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Get all questions created by user
    public async Task<IEnumerable<Question>> GetAllUserQuestionsAsync(int id)
    {
        try
        {
            var questions = (await questionRepository.ReadRangeAsync(q => q.User != null && q.UserID == id)).Select(entity => (Question)entity) ?? null!;
            return questions;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // Get Questions - Difficulty/Category/Language
    public async Task<IEnumerable<Question>> GetQuestionsAsync(Expression<Func<QuestionEntity, bool>> predicate)
    {
        try
        {
            var questions = (await questionRepository.ReadRangeAsync(predicate)).Select(entity => (Question)entity);
            return questions;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
}
