namespace NASAProj.Service.DTOs.Quizzes
{
    public class QuestionAnswerForCreationDTO
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
