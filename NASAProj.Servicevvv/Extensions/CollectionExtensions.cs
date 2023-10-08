using NASAProj.Domain.Configurations;
using NASAProj.Service.Helpers;
using Newtonsoft.Json;

namespace NASAProj.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParams @params) where T : class
        {
            if (@params is null)
                @params = new PaginationParams();

            var paginationMetaData = new PaginationMetaData(source.Count(), @params.PageSize, @params.PageIndex);

            if (HttpContextHelper.ReponseHeaders.Keys.Contains("X-Pagination"))
                HttpContextHelper.ReponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ReponseHeaders.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetaData));

            HttpContextHelper.ReponseHeaders.Add("Access-Control-Expose-Headers", "X-Pagination");

            return @params.PageSize >= 0 && @params.PageIndex > 0
                ? source.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize)
                : source;
        }

        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params) where T : class
        {
            @params ??= new PaginationParams();

            var paginationMetaData = new PaginationMetaData(source.Count(), @params.PageSize, @params.PageIndex);

            if (HttpContextHelper.ReponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ReponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ReponseHeaders.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetaData));

            HttpContextHelper.ReponseHeaders.Add("Access-Control-Expose-Headers", "X-Pagination");

            return @params.PageSize >= 0 && @params.PageIndex > 0
                ? source.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize)
                : source;
        }
    }
}
