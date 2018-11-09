using BF.POC.FaceAPI.Domain.Entities;
using System;

namespace BF.POC.FaceAPI.Domain.Contracts
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Person GetByAPIPersonId(Guid apiPersonId);
    }
}
