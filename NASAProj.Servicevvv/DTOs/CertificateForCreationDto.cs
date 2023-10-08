using ZaminEducation.Service.DTOs.UserCourses;

namespace NASAProj.Service.DTOs
{
    public class CertificateForCreationDTO
    {
        public long CourseId { get; set; }

        public long? UserId { get; set; }

        public CertificateResultDTO Result { get; set; }
    }
}
