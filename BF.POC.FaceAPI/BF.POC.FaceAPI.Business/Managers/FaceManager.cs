using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Domain.Contracts.Clients;
using BF.POC.FaceAPI.Domain.Entities;
using BF.POC.FaceAPI.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Business
{
    public class FaceManager : BaseManager, IFaceManager
    {
        private readonly IGroupRepository groupRepository;
        private readonly IPersonRepository personRepository;
        private readonly IFaceRepository faceRepository;
        private readonly IFaceAPIClient faceAPIClient;

        public FaceManager(IGroupRepository groupRepository, IPersonRepository personRepository, IFaceRepository faceRepository, IFaceAPIClient faceAPIClient)
        {
            this.groupRepository = groupRepository;
            this.personRepository = personRepository;
            this.faceRepository = faceRepository;
            this.faceAPIClient = faceAPIClient;
        }

        public IList<Face> GetAll()
        {
            return faceRepository.GetAll().ToList();
        }

        public IList<Face> GetAllByPersonId(int personId)
        {
            return faceRepository.GetAll().Where(face => face.PersonId == personId).ToList();
        }

        public IList<Face> GetAllByGroupId(int groupId)
        {
            return faceRepository.GetAll().Where(face => face.Person.GroupId == groupId).ToList();
        }

        public Face GetById(int id)
        {
            return faceRepository.GetById(id);
        }

        public async Task AddAsync(Face face)
        {
            var person = personRepository.GetById(face.PersonId);
            var group = groupRepository.GetById(person.GroupId);

            var faces = await faceAPIClient.FaceCountFacesAsync(face.Photo);

            if (faces.Length == 0)
            {
                throw new BusinessException("No faces found in the selected image");
            }

            if (faces.Length > 1)
            {
                throw new BusinessException("To many faces found in the selected image");
            }

            // ToDo: Call faceAPIClient.FaceAddAsync() here, get FaceId and assign it to face.APIFaceId property...
            throw new System.NotImplementedException();

            // ToDo: Call faceRepository.AddAsync() here...
            throw new System.NotImplementedException();

            // ToDo: Call faceAPIClient.GroupTrainAsync() here...
            throw new System.NotImplementedException();
        }

    }
}
