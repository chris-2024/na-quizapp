using Moq;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Repositories;
using System.Linq.Expressions;

namespace QuizApp.Tests.UnitTests;

public class QuestionRepositoryTests
{
    [Fact]
    public async Task ReadRangeAsync_ShouldIncludeAnswers()
    {
        // Arrange
        var questionEntities = new List<QuestionEntity>
        {
            new QuestionEntity()
            {
                QuestionID = 1,
                QuestionText = "Question 1",
                Answers = new List<AnswerEntity>
                {
                    new AnswerEntity() { AnswerID = 1, QuestionID = 1, AnswerText = "Answer 1", IsCorrect = true },
                    new AnswerEntity() { AnswerID = 2, QuestionID = 1, AnswerText = "Answer 2", IsCorrect = false },
                }
            },
            new QuestionEntity()
            {
                QuestionID = 2,
                QuestionText = "Question 2",
                Answers = new List<AnswerEntity>
                {
                    new AnswerEntity() { AnswerID = 3, QuestionID = 2, AnswerText = "Answer 3", IsCorrect = true },
                    new AnswerEntity() { AnswerID = 4, QuestionID = 2, AnswerText = "Answer 4", IsCorrect = false },
                }
            }
        };

        var mockQuestionRepository = new Mock<IQuestionRepository>();
        mockQuestionRepository
        .Setup(repo => repo.ReadRangeAsync(It.IsAny<Expression<Func<QuestionEntity, bool>>>()))
        .ReturnsAsync((Expression<Func<QuestionEntity, bool>> predicate) =>
            questionEntities.Where(predicate.Compile()));


        // Act
        var result = await mockQuestionRepository.Object.ReadRangeAsync(q => q.QuestionID == 1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); // Assuming only one question matches the predicate
        var question = result.First();
        Assert.NotNull(question.Answers);
        Assert.Equal(2, question.Answers.Count()); // Assuming two answers for the first question
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteQuestion()
    {
        // Arrange
        var questionEntities = new List<QuestionEntity>
    {
        new QuestionEntity { QuestionID = 1, QuestionText = "Question 1", Answers = new List<AnswerEntity>() },
        new QuestionEntity { QuestionID = 2, QuestionText = "Question 2", Answers = new List<AnswerEntity>() },

    };

        var mockQuestionRepository = new Mock<IQuestionRepository>();
        mockQuestionRepository
            .Setup(repo => repo.DeleteAsync(It.IsAny<QuestionEntity>()))
            .ReturnsAsync((QuestionEntity question) =>
            {
                var deleted = questionEntities.Remove(question);
                return deleted;
            });

        // Act
        var result = await mockQuestionRepository.Object.DeleteAsync(questionEntities.First());

        // Assert
        Assert.True(result); // Assuming the delete operation is successful
        Assert.Single(questionEntities); // Assuming one question is deleted
    }
}