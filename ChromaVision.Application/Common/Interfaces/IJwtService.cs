using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(Guid userId, string username);
        Task<(bool isValid, Guid userId)> ValidateTokenAsync(string token);
    }
}
