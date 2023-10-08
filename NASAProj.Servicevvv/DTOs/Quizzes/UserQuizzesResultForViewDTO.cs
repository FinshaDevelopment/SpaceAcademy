
using ZaminEducation.Service.DTOs.Quizzes;

namespace ZaminEducation.Service.ViewModels.Quizzes;

public class UserQuizzesResultForViewDTO
{
    public double Persentage { get; set; }
    public int QuizzesCount { get; set; }
    public int AcceptedQuizzes { get; set; }

    public IEnumerable<QuizForViewDTO> quizResultViewModels { get; set; }
}