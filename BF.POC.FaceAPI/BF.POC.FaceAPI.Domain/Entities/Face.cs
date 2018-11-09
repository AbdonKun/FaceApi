using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BF.POC.FaceAPI.Domain.Entities
{
    public class Face : BaseEntity
    {
        [Required]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required]
        public Guid APIFaceId { get; set; }

        [Required]
        public byte[] Photo { get; set; }

    }
}
