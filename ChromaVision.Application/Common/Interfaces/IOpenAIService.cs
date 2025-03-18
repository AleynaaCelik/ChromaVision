using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Interfaces
{
    public interface IOpenAIService
    {
        Task<List<string>> GenerateColorsFromDescriptionAsync(string description, int colorCount = 5, CancellationToken cancellationToken = default);
    }
}
