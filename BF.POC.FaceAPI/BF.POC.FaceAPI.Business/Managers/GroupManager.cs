using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Domain.Contracts.Clients;
using BF.POC.FaceAPI.Domain.DTOs;
using BF.POC.FaceAPI.Domain.Entities;
using BF.POC.FaceAPI.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Business
{
    public class GroupManager : BaseManager, IGroupManager
    {
        private readonly IGroupRepository groupRepository;
        private readonly IPersonRepository personRepository;
        private readonly IFaceAPIClient faceAPIClient;

        public GroupManager(IGroupRepository groupRepository, IPersonRepository personManager, IFaceAPIClient faceAPIClient)
        {
            this.groupRepository = groupRepository;
            this.personRepository = personManager;
            this.faceAPIClient = faceAPIClient;
        }

        public IList<Group> GetAll()
        {
            return groupRepository.GetAll().ToList();
        }

        public Group GetById(int id)
        {
            return groupRepository.GetById(id);
        }

        public async Task AddAsync(Group group)
        {
            if (!(await faceAPIClient.GroupExistsAsync(group.Code)))
            {
                // ToDo: Call faceAPIClient.GroupCreateAsync() here...
                throw new System.NotImplementedException();

                await groupRepository.AddAsync(group);
            }
            else
            {
                throw new BusinessException($"Person group '{group.Code}' already exists.");
            }
        }

        public async Task UpdateAsync(Group group)
        {
            // ToDo: Call faceAPIClient.GroupUpdateAsync() here...
            throw new System.NotImplementedException();

            await groupRepository.UpdateAsync(group);
        }

        public async Task<List<Candidate>> SearchCandidatesAsync(int id, byte[] image)
        {
            var group = groupRepository.GetById(id);

            Microsoft.ProjectOxford.Face.Contract.Face[] faces;

            // ToDo: Call faceAPIClient.FaceDetectAsync() here and assing its result into 'faces' var...
            throw new System.NotImplementedException();

            if (faces.Length > 0)
            {
                var candidates = faces.Select(c => new Candidate { Gender = c.FaceAttributes.Gender, FaceRectangle = c.FaceRectangle }).ToList();
                var faceIDs = faces.Select(p => p.FaceId).ToArray();

                Microsoft.ProjectOxford.Face.Contract.IdentifyResult[] identifyResult;

                // ToDo: Call faceAPIClient.FaceIdentifyFacesAsync() here and assing its result into 'identifyResult' var...
                throw new System.NotImplementedException();

                for (int i = 0; i < identifyResult.Length; i++)
                {
                    var result = identifyResult[i];

                    if (result.Candidates.Length > 0)
                    {
                        var candidate = result.Candidates[0];
                        var person = personRepository.GetByAPIPersonId(candidate.PersonId);

                        if (person != null)
                        {
                            candidates[i].AssociateWith(person);

                            // ToDo: Save the value from candidate.Confidence into current Candidate object here (candidates[i].Confidence)...
                            throw new System.NotImplementedException();
                        }
                    }
                }

                return candidates;
            }
            else
            {
                return new List<Candidate>();
            }
        }

    }
}
