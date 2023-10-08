namespace NASAProj.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(string username, string password);
    }
}
