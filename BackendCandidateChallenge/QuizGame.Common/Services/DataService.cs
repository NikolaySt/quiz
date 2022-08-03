using Microsoft.EntityFrameworkCore;

namespace QuizGame.Common.Services
{
    public abstract class DataService<TEntity, TDbContext> : IDataService<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected DataService(TDbContext db) => Data = db;

        protected TDbContext Data { get; }

        protected IQueryable<TEntity> All() => Data.Set<TEntity>();

        public async Task Save(
            TEntity entity)
        {
            Data.Update(entity);

            await Data.SaveChangesAsync();
        }
    }
}