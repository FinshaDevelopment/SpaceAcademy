using NASAProj.Domain.Configurations;
using NASAProj.Domain.Entities.Quizzes;
using System.Linq.Expressions;
using NASAProj.Service.DTOs.Quizzes;
using NASAProj.Service.ViewModels.Quizzes;

namespace NASAProj.Service.Interfaces
{
    public interface IQuizResultService
    {
        ValueTask<QuizResult> GetAsync(Expression<Func<QuizResult, bool>> expression);
        ValueTask<IEnumerable<QuizResult>> GetAllAsync(Expression<Func<QuizResult, bool>> expression, PaginationParams @params);
        ValueTask<UserQuizzesResultForViewDTO> CreateAsync(IEnumerable<UserSelectionDTO> dto);
    }
}
