using NASAProj.Domain.Common;
using NASAProj.Domain.Models;

namespace NASAProj.Domain.Entities.Quizzes
{
    public class QuizResult : Auditable
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public double Percentage { get; set; }
    }
}