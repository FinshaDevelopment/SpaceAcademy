
using NASAProj.Service.DTOs.Quizzes;

namespace NASAProj.Service.ViewModels.Quizzes;

public class UserQuizzesResultForViewDTO
{
    public double Persentage { get; set; }
    public int QuizzesCount { get; set; }
    public int AcceptedQuizzes { get; set; }

    public IEnumerable<QuizForViewDTO> quizResultViewModels { get; set; }
}