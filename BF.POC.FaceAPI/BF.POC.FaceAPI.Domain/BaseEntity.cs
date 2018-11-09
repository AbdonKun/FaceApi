using System.ComponentModel.DataAnnotations;

namespace BF.POC.FaceAPI.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

    }
}
