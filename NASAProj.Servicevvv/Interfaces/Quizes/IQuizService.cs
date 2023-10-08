using NASAProj.Domain.Entities.Quizzes;
using System.Linq.Expressions;
using NASAProj.Service.DTOs.Quizzes;

namespace NASAProj.Service.Interfaces
{
    public interface IQuizService
    {
        ValueTask<Quiz> CreateAsync(QuizForCreationDTO quizForCreationDto);
        ValueTask<QuestionAnswer> CreateAnswerAsync(QuestionAnswerForCreationDTO answerForCreationDto);
        ValueTask<Quiz> UpdateAsync(int quizId,
            QuizForCreationDTO quizForCreationDto);

        ValueTask<IEnumerable<Quiz>> GetAllAsync(Expression<Func<Quiz, bool>> expression = null);
        ValueTask<Quiz> GetAsync(int quizId);
        ValueTask<QuestionAnswer> UpdateAnswerAsync(int answerId, QuestionAnswerForCreationDTO answerForCreationDto);
        ValueTask<bool> DeleteAnswerAsync(Expression<Func<QuestionAnswer, bool>> expression);
        ValueTask<bool> DeleteAsync(Expression<Func<Quiz, bool>> expression);
    }
}