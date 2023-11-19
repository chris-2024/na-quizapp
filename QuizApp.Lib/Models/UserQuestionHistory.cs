using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Models;

public class UserQuestionHistory
{
    public Question Question { get; set; } = null!;
    public DateTime AnsweredDate { get; set; }

    // Convert UserQuestionHistoryEntity to UserQuestionHistory
    public static implicit operator UserQuestionHistory(UserQuestionHistoryEntity questionHistory)
    {
        return new UserQuestionHistory()
        {
            Question = questionHistory.Question,
            AnsweredDate = questionHistory.AnsweredDate
        };
    }
}
