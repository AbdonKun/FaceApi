using BF.POC.FaceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Models
{
    public class FaceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Person")]
        [Required(ErrorMessage = "The field 'Person' is mandatory")]
        public int PersonId { get; set; }

        public PersonViewModel Person { get; set; }

        [Display(Name = "Photo")]
        [Required(ErrorMessage = "The field 'Photo' is mandatory")]
        public byte[] Photo { get; set; }

        public Guid APIFaceId { get; set; }

        public IEnumerable<SelectListItem> PersonList { get; set; }
        
        public FaceViewModel()
        {
            PersonList = new List<SelectListItem>();
        }

        public FaceViewModel(Face faceEntity)
        {
            Id = faceEntity.Id;
            PersonId = faceEntity.PersonId;
            Person = new PersonViewModel(faceEntity.Person);
            Photo = faceEntity.Photo;
            APIFaceId = faceEntity.APIFaceId;
        }

        public Face ToEntity()
        {
            return new Face
            {
                Id = Id,
                PersonId = PersonId,
                Photo = Photo
            };
        }

    }
}