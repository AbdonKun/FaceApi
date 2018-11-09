using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BF.POC.FaceAPI.Domain.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [NotMapped]
        public string Fullname { get { return $"{Name} {Lastname}"; } }

        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        [Required]
        public Guid APIPersonId { get; set; }

    }
}