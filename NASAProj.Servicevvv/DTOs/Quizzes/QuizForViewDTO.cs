using NASAProj.Domain.Entities.Quizzes;

namespace ZaminEducation.Service.DTOs.Quizzes;

public class QuizForViewDTO
{
    public long Id { get; set; }
    public Quiz Quiz { get; set; }
    public QuestionAnswer Choice { get; set; }
    public bool IsCorrect { get; set; }
}