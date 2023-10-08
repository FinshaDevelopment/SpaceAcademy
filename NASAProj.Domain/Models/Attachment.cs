using NASAProj.Domain.Common;

namespace NASAProj.Domain.Models
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
