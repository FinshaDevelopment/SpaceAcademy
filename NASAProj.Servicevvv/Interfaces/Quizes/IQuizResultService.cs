using NASAProj.Domain.Configurations;
using NASAProj.Domain.Entities.Quizzes;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Quizzes;
using ZaminEducation.Service.ViewModels.Quizzes;

namespace ZaminEducation.Service.Interfaces
{
    public interface IQuizResultService
    {
        ValueTask<QuizResult> GetAsync(Expression<Func<QuizResult, bool>> expression);
        ValueTask<IEnumerable<QuizResult>> GetAllAsync(Expression<Func<QuizResult, bool>> expression, PaginationParams @params);
        ValueTask<UserQuizzesResultForViewDTO> CreateAsync(IEnumerable<UserSelectionDTO> dto);
    }
}
