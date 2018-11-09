using System.ComponentModel.DataAnnotations;

namespace BF.POC.FaceAPI.Domain.Entities
{
    public class Group : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

    }
}