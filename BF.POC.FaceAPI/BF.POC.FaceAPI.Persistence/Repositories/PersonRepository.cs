using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace BF.POC.FaceAPI.Persistence.Repositories
{
    public class PersonRepository: BaseRepository<Person>, IPersonRepository
    {
        public override IQueryable<Person> GetAll()
        {
            return base.GetAll()
                .Include(person => person.Group);
        }

        public Person GetByAPIPersonId(Guid apiPersonId)
        {
            return GetAll().Where(person => person.APIPersonId == apiPersonId).FirstOrDefault();
        }

    }
}
