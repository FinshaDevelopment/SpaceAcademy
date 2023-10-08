using NASAProj.Service.DTOs.UserCourses;

namespace NASAProj.Service.DTOs
{
    public class CertificateForCreationDTO
    {
        public int CourseId { get; set; }

        public int? UserId { get; set; }

        public CertificateResultDTO Result { get; set; }
    }
}
