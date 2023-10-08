using NASAProj.Domain.Entities.Quizzes;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Quizzes;

namespace ZaminEducation.Service.Interfaces
{
    public interface IQuizService
    {
        ValueTask<Quiz> CreateAsync(QuizForCreationDTO quizForCreationDto);
        ValueTask<QuestionAnswer> CreateAnswerAsync(QuestionAnswerForCreationDTO answerForCreationDto);
        ValueTask<Quiz> UpdateAsync(long quizId,
            QuizForCreationDTO quizForCreationDto);

        ValueTask<IEnumerable<Quiz>> GetAllAsync(Expression<Func<Quiz, bool>> expression = null);
        ValueTask<Quiz> GetAsync(long quizId);
        ValueTask<QuestionAnswer> UpdateAnswerAsync(long answerId, QuestionAnswerForCreationDTO answerForCreationDto);
        ValueTask<bool> DeleteAnswerAsync(Expression<Func<QuestionAnswer, bool>> expression);
        ValueTask<bool> DeleteAsync(Expression<Func<Quiz, bool>> expression);
    }
}