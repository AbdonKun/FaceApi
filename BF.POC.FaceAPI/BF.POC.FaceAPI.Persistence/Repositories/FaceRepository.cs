using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Domain.Entities;
using System.Data.Entity;
using System.Linq;

namespace BF.POC.FaceAPI.Persistence.Repositories
{
    public class FaceRepository : BaseRepository<Face>, IFaceRepository
    {
        public override IQueryable<Face> GetAll()
        {
            return base.GetAll()
                .Include(face => face.Person)
                .Include(face => face.Person.Group);
        }

    }
}
