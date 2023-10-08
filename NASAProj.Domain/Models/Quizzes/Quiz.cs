using NASAProj.Domain.Common;

namespace NASAProj.Domain.Entities.Quizzes
{
    public class Quiz : Auditable
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}