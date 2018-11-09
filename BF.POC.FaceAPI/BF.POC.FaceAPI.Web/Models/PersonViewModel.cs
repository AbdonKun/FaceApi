using BF.POC.FaceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field '{0}' is mandatory")]
        [StringLength(50, ErrorMessage = "The field '{0}' must have a maximum of {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field '{0}' is mandatory")]
        [StringLength(50, ErrorMessage = "The field '{0}' must have a maximum of {1} characters")]
        public string Lastname { get; set; }

        public string Fullname { get { return $"{Name} {Lastname}"; } }

        [Display(Name = "Group")]
        [Required(ErrorMessage = "The field 'Group' is mandatory")]
        public int GroupId { get; set; }

        public GroupViewModel Group { get; set; }

        public Guid APIPersonId { get; set; }

        public IEnumerable<SelectListItem> GroupList { get; set; }
        
        public PersonViewModel()
        {
            GroupList = new List<SelectListItem>();
        }

        public PersonViewModel(Person personEntity)
        {
            Id = personEntity.Id;
            Name = personEntity.Name;
            Lastname = personEntity.Lastname;
            GroupId = personEntity.GroupId;
            Group = new GroupViewModel(personEntity.Group);
            APIPersonId = personEntity.APIPersonId;
        }

        public Person ToEntity()
        {
            return new Person
            {
                Id = Id,
                Name = Name,
                Lastname = Lastname,
                GroupId = GroupId
            };
        }

    }
}