using NASAProj.Domain.Configurations;
using NASAProj.Domain.Models;
using NASAProj.Service.DTOs;
using NASAProj.Service.DTOs.Users;
using NASAProj.Service.DTOs.Users.API;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Users;

namespace NASAProj.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> CreateAsync(UserForCreationDTO dto);
        ValueTask<User> UpdateAsync(long id, UserForUpdateDTO dto);
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> GetInfoAsync();
        ValueTask<User> ChangePasswordAsync(UserForChangePasswordDTO dto);
        ValueTask<User> AddAttachmentAsync(long id, AttachmentForCreationDTO attachmentForCreationDto);
    }
}
