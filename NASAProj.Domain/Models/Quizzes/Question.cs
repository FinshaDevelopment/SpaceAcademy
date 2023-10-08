using NASAProj.Domain.Common;

namespace NASAProj.Domain.Entities.Quizzes
{
    public class Question : Auditable
    {
        public string Text { get; set; }
        public long QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public virtual ICollection<QuestionAnswer> Answers { get; set; }
    }
}