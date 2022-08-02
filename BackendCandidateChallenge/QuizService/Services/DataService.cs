using System.Linq;
using System.Threading.Tasks;
using QuizService.Data;

namespace QuizService.Services
{
    public abstract class DataService<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        protected DataService(QuizDbContext db) => Data = db;

        protected QuizDbContext Data { get; }

        protected IQueryable<TEntity> All() => Data.Set<TEntity>();

        public async Task Save(
            TEntity entity)
        {
            Data.Update(entity);

            await Data.SaveChangesAsync();
        }
    }
}