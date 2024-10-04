using CustomMessenger.Service.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomMessenger.Service.DTO.Groups
{
    public class GroupForUpdate
    {
        [Required]
        public Guid Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, ValidUniquename]
        public string UniqueName { get; set; }
    }
}
