using ChromaVision.Application.Common.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.ColorPalettes.Commands
{
    public class CreatePaletteCommand : IRequest<Result<ColorPaletteDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Colors { get; set; } = new List<string>();
        public Guid? UserId { get; set; }
    }
}
