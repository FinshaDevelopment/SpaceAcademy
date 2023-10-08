using Microsoft.AspNetCore.Http;

namespace NASAProj.Service.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor;
        public static HttpContext Context => Accessor.HttpContext;
        public static IHeaderDictionary ReponseHeaders => Context?.Response?.Headers;
        public static int? UserId => GetUserId();
        public static string Role => Context?.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        public static string HostUrl => Context?.Request?.Scheme + "://" + Context?.Request?.Host.Value;

        private static int? GetUserId()
        {
            string value = Context?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value;

            bool canParse = int.TryParse(value, out int id);
            return canParse ? id : null;
        }
    }
}
