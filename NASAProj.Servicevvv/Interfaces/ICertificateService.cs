using NASAProj.Domain.Configurations;
using NASAProj.Domain.Models;
using NASAProj.Service.DTOs;
using System.Linq.Expressions;

namespace ZaminEducation.Service.Interfaces
{
    public interface ICertificateService
    {
        public ValueTask<Attachment> CreateAsync(CertificateForCreationDTO certificateForCreation);
        public ValueTask<IEnumerable<Certificate>> GetAllAsync(PaginationParams @params,
            Expression<Func<Certificate, bool>> expression = null);
        public ValueTask<Certificate> GetAsync(Expression<Func<Certificate, bool>> expression);
    }
}
