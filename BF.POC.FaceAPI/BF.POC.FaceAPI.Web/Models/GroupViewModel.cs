using BF.POC.FaceAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BF.POC.FaceAPI.Web.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field '{0}' is mandatory")]
        [StringLength(20, ErrorMessage = "The field '{0}' must have a maximum of {1} characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The field '{0}' is mandatory")]
        [StringLength(50, ErrorMessage = "The field '{0}' must have a maximum of {1} characters")]
        public string Name { get; set; }

        public GroupViewModel()
        {
        }

        public GroupViewModel(Group groupEntity)
        {
            Id = groupEntity?.Id ?? 0;
            Code = groupEntity?.Code ?? "-";
            Name = groupEntity?.Name ?? "-";
        }

        public Group ToEntity()
        {
            return new Group
            {
                Id = Id,
                Code = Code,
                Name = Name
            };
        }

    }
}