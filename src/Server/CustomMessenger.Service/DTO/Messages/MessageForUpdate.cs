using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.DTO.Messages
{
    public class MessageForUpdate
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
