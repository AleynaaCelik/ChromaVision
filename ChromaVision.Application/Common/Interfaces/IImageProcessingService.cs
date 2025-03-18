using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Interfaces
{
    public interface IImageProcessingService
    {
        Task<List<string>> ExtractColorsFromImageAsync(Stream imageStream, int colorCount = 5, CancellationToken cancellationToken = default);
    }
}
