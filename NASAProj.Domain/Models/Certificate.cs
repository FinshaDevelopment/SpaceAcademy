using NASAProj.Domain.Common;

namespace NASAProj.Domain.Models
{
    public class Certificate : Auditable
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
