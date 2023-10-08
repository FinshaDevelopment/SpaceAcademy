using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NASAProj.Data.IRepositories;
using NASAProj.Domain.Configurations;
using NASAProj.Domain.Models;
using NASAProj.Service.DTOs;
using NASAProj.Service.DTOs.Users;
using NASAProj.Service.Exceptions;
using NASAProj.Service.Extensions;
using NASAProj.Service.Helpers;
using NASAProj.Service.Interfaces;
using System.Linq.Expressions;

namespace NASAProj.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        public UserService(IGenericRepository<User> userRepository,
            IMapper mapper,
            IAttachmentService attachmentService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }

        public async ValueTask<User> CreateAsync(UserForCreationDTO dto)
        {
            var user = await userRepository.GetAsync(u => u.Email == dto.Email);

            if (user is not null)
                throw new HttpStatusCodeException(400, "User already exists");

            User mappedUser = mapper.Map<User>(dto);

            mappedUser.Password = dto.Password.Encrypt();

            User newUser = await userRepository.AddAsync(mappedUser);

            await userRepository.SaveChangesAsync();

            return newUser;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression);

            if (user is null)
                throw new HttpStatusCodeException(404, "User not found");

            userRepository.Delete(user);

            await userRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null, string search = null)
        {
            var users = userRepository.GetAll(expression, "Image", isTracking: false);

            return !string.IsNullOrEmpty(search)
                ? users.Where(u => u.FirstName == search ||
                        u.LastName == search ||
                        u.Email == search)
                : (IEnumerable<User>) users.ToPagedList(@params).ToListAsync();
        }

        public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression, "Address, Image" );

            if (user is null)
                throw new HttpStatusCodeException(404, "User not found");

            return user;
        }

        public async ValueTask<User> UpdateAsync(int id, UserForUpdateDTO dto)
        {
            var user = await userRepository.GetAsync(u => u.Id == id);

            if (user is null)
                throw new HttpStatusCodeException(404, "User not found!");

            var alredyExistsUser = await userRepository.GetAsync(u => u.Email == dto.Email && u.Id != id);
            if (alredyExistsUser is not null)
                throw new HttpStatusCodeException(400, "Login or Password is incorrect!");

            user = mapper.Map(dto, user);

            user.Password = user.Password.Encrypt();
            user.Update();

            userRepository.Update(entity: user);

            await userRepository.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> GetInfoAsync()
            => await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId, includes: "Image");

        public async ValueTask<User> AddAttachmentAsync(int userId, AttachmentForCreationDTO attachmentForCreationDto)
        {
            var attachment = await attachmentService.UploadAsync(attachmentForCreationDto);

            var user = await userRepository.GetAsync(u => u.Id == userId);

            if (user == null)
                throw new HttpStatusCodeException(404, "User not found");

            user.ImageId = attachment.Id;

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> ChangePasswordAsync(UserForChangePasswordDTO dto)
        {
            User existUser = await userRepository.GetAsync(user => user.Email == dto.Email);

            if (existUser is null)
                throw new Exception("This Username does not exist");

            else if (dto.NewPassword != dto.ComfirmPassword)
                throw new Exception("New password and comfirm password are not equal");

            else if (existUser.Password != dto.OldPassword.Encrypt())
                throw new Exception("Password is incorrect!");

            existUser.Password = dto.NewPassword.Encrypt();
            await userRepository.SaveChangesAsync();

            return existUser;
        }
    }
}
