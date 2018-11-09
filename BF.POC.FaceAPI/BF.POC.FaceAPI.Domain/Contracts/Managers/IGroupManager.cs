using BF.POC.FaceAPI.Domain.DTOs;
using BF.POC.FaceAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Domain.Contracts
{
    public interface IGroupManager
    {
        IList<Group> GetAll();

        Group GetById(int id);

        Task AddAsync(Group group);

        Task UpdateAsync(Group group);

        Task<List<Candidate>> SearchCandidatesAsync(int id, byte[] image);
    }
}
