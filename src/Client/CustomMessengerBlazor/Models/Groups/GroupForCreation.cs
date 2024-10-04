using CustomMessengerBlazor.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomMessenger.Service.DTO.Groups
{
    public class GroupForCreation
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, ValidUniquename]
        public string UniqueName { get; set; }
    }
}
