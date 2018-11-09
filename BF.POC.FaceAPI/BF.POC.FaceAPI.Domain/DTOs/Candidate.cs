using BF.POC.FaceAPI.Domain.Entities;
using Microsoft.ProjectOxford.Face.Contract;
using System;

namespace BF.POC.FaceAPI.Domain.DTOs
{
    public class Candidate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get { return $"{Name} {Lastname}"; } }

        public Group Group { get; set; }

        public Guid APIPersonId { get; set; }

        public string Gender { get; set; }

        public FaceRectangle FaceRectangle { get; set; }

        public double Confidence { get; set; }

        public void AssociateWith(Entities.Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Lastname = person.Lastname;
            Group = person.Group;
            APIPersonId = person.APIPersonId;
        }
    }
}
