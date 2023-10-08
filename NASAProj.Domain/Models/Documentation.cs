using NASAProj.Domain.Common;

namespace NASAProj.Domain.Models
{
    public class Documentation : Auditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
