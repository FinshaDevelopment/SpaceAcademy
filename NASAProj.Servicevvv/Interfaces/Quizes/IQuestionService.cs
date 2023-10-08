using NASAProj.Domain.Configurations;
using NASAProj.Domain.Entities.Quizzes;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Quizzes;

namespace ZaminEducation.Service.Interfaces
{
    public interface IQuestionService
    {
        ValueTask<Question> CreateAsync(QuestionForCreationDTO questionForCreationDTO);

        ValueTask<Question> UpdateAsync(int id, QuestionForCreationDTO questionForCreationDTO);

        ValueTask<bool> DeleteAsync(int id);

        ValueTask<IEnumerable<Question>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<Question, bool>> expression = null);

        ValueTask<Question> GetAsync(Expression<Func<Question, bool>> expression);
    }
}
