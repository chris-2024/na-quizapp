using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Lib.Models;

public class Question
{
    public string QuestionText { get; set; } = null!;

    public CategoryEntity Category { get; set; } = null!;

    public Difficulty Difficulty { get; set; }

    public Language Language { get; set; }

    public UserEntity? User { get; set; }

    public List<AnswerEntity>? Answers { get; set; }

    // Convert UserQuestionHistoryEntity to UserQuestionHistory
    public static implicit operator Question(QuestionEntity questionHistory)
    {
        return new Question()
        {
            QuestionText = questionHistory.QuestionText,
            Category = questionHistory.Category,
            Difficulty = (Difficulty)questionHistory.DifficultyID,
            Language = (Language)questionHistory.LanguageID,
            User = questionHistory.User!,
            Answers = new List<AnswerEntity>(questionHistory.Answers!)
        };
    }
}
