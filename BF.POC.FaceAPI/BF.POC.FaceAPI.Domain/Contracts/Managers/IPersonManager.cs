using BF.POC.FaceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Domain.Contracts
{
    public interface IPersonManager
    {
        IList<Person> GetAll();

        IList<Person> GetAllByGroupId(int groupId);

        Person GetById(int id);

        Task AddAsync(Person person);

        Task UpdateAsync(Person person);

    }
}
