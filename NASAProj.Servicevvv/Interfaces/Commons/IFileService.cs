using Microsoft.AspNetCore.Http;

namespace NASAProj.Service.Interfaces.Commons
{
    public interface IFileService
    {
        Task<TDestination> GetJsonAsync<TDestination>(string path);
        Task SaveJsonAsync(string path, object data);
        Task<bool> SaveAsync(string path, IFormFile file);
        void Delete(string path);
    }
}
