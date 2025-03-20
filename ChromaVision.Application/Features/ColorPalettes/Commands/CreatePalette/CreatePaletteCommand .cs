using ChromaVision.Application.Common.Mappings;
using ChromaVision.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.ColorPalettes.Commands.CreatePalette
{
    public class CreatePaletteCommand : IRequest<Result<ColorPaletteDto>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Colors { get; set; } = new List<string>();
        public Guid? UserId { get; set; }
    }
}
