using BF.POC.FaceAPI.Domain;
using NLog;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Persistence
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity 
    {
        // Common properties
        protected readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private DbContext context;

        protected DbContext Context
        {
            get { return this.context ?? (this.context = new FaceAPIContext()); }
            set { this.context = value; }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

    }
}
