using CustomMessenger.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
