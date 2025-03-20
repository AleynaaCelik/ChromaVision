using ChromaVision.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.ColorPalettes.Commands.DeletePalette
{
    public class DeletePaletteCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

}
