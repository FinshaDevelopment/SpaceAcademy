using Microsoft.EntityFrameworkCore;
using NASAProj.Data.DbContexts;
using NASAProj.Data.IRepositories;
using System.Linq.Expressions;

namespace NASAProj.Data.Repositories
{
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        protected readonly NASAProjDbContext dbContext;
        protected readonly DbSet<TSource> dbSet;

        public GenericRepository(NASAProjDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TSource>();
        }

        public async ValueTask<TSource> AddAsync(TSource entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public void Delete(TSource entity)
            => dbSet.Remove(entity);

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null
            , bool isTracking = true)
        {
            IQueryable<TSource> query = expression is null ? dbSet : dbSet.Where(expression);

            if (!isTracking)
                query = query.AsNoTracking();

            if (!string.IsNullOrEmpty(include))
                foreach (var i in include.Split(','))
                    query = query.Include(i.Trim());

            return query;
        }

        public async ValueTask<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string includes = null)
            => await GetAll(expression, includes).FirstOrDefaultAsync();

        public TSource Update(TSource entity)
            => dbSet.Update(entity).Entity;

        public async ValueTask SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
