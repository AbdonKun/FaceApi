using BF.POC.FaceAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Domain.Contracts
{
    public interface IFaceManager
    {
        IList<Face> GetAll();

        IList<Face> GetAllByPersonId(int personId);

        IList<Face> GetAllByGroupId(int groupId);

        Face GetById(int id);

        Task AddAsync(Face face);

    }
}
