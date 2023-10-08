namespace NASAProj.Domain.Common
{
    public class Auditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
