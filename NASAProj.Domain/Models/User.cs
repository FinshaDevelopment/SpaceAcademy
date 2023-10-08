using NASAProj.Domain.Common;
using NASAProj.Domain.Entities.Quizzes;

namespace NASAProj.Domain.Models
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ImageId { get; set; }
        public Attachment Image { get; set; }
        public ICollection<QuizResult> QuizResults { get; set;}
    }
}
