using NASAProj.Domain.Entities.Quizzes;

namespace NASAProj.Service.DTOs.Quizzes;

public class QuizForViewDTO
{
    public int Id { get; set; }
    public Quiz Quiz { get; set; }
    public QuestionAnswer Choice { get; set; }
    public bool IsCorrect { get; set; }
}