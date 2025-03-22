using ChromaVision.Application.Common.Mappings;
using ChromaVision.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<UserDto>>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
