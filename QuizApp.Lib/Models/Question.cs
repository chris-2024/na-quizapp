using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Models;

public class Question
{
    public int QuestionID { get; set; }

    public string QuestionText { get; set; } = null!;

    public CategoryEntity Category { get; set; } = null!;

    public Difficulty Difficulty { get; set; }

    public Language Language { get; set; }

    public int UserID { get; set; }

    public List<AnswerEntity>? Answers { get; set; }

    // Convert QuestionEntity to Question
    public static implicit operator Question(QuestionEntity question)
    {
        return new Question()
        {
            QuestionID = question.QuestionID,
            QuestionText = question.QuestionText,
            Category = question.Category,
            Difficulty = (Difficulty)question.DifficultyID,
            Language = (Language)question.LanguageID,
            UserID = question.UserID,
            Answers = new List<AnswerEntity>(question.Answers!)
        };
    }
}