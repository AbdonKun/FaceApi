using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Domain.Contracts.Clients;
using BF.POC.FaceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Business
{
    public class PersonManager : BaseManager, IPersonManager
    {
        private readonly IGroupRepository groupRepository;
        private readonly IPersonRepository personRepository;
        private readonly IFaceAPIClient faceAPIClient;

        public PersonManager(IGroupRepository groupRepository, IPersonRepository personRepository, IFaceAPIClient faceAPIClient)
        {
            this.groupRepository = groupRepository;
            this.personRepository = personRepository;
            this.faceAPIClient = faceAPIClient;
        }

        public IList<Person> GetAll()
        {
            return personRepository.GetAll().ToList();
        }

        public IList<Person> GetAllByGroupId(int groupId)
        {
            return personRepository.GetAll().Where(person => person.GroupId == groupId).ToList();
        }

        public Person GetById(int id)
        {
            return personRepository.GetById(id);
        }

        public async Task AddAsync(Person person)
        {
            var group = groupRepository.GetById(person.GroupId);

            // ToDo: Call faceAPIClient.PersonCreateAsync() here, get PersonId and assign it to person.APIPersonId property...
            throw new NotImplementedException();

            // ToDo: Call personRepository.AddAsync() here...
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Person person)
        {
            var group = groupRepository.GetById(person.GroupId);

            // ToDo: Call faceAPIClient.PersonUpdateAsync() here...
            throw new NotImplementedException();

            // ToDo: Call personRepository.UpdateAsync() here...
            throw new NotImplementedException();
        }

    }
}
