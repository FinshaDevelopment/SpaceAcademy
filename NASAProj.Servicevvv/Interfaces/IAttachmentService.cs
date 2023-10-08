using NASAProj.Domain.Models;
using NASAProj.Service.DTOs;
using System.Linq.Expressions;

namespace ZaminEducation.Service.Interfaces;

public interface IAttachmentService
{
    ValueTask<Attachment> UploadAsync(AttachmentForCreationDTO dto);
    ValueTask<Attachment> UpdateAsync(long id, Stream stream);
    ValueTask<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression);
    ValueTask<Attachment> GetAsync(Expression<Func<Attachment, bool>> expression);
    ValueTask<Attachment> CreateAsync(string fileName, string filePath);
}
