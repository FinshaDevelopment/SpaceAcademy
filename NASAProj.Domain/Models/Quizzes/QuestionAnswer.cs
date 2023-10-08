using NASAProj.Domain.Common;

namespace NASAProj.Domain.Entities.Quizzes
{
    public class QuestionAnswer : Auditable
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}