using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Mappings
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; } // required anahtar kelimesi ekleyin
        public required string Email { get; set; }    // required anahtar kelimesi ekleyin
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
