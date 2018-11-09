using System.Linq;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Domain
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

    }
}
