using ChromaVision.Core.Interfaces;
using ChromaVision.Core.Models;
using ChromaVision.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.ColorPalettes.Commands.DeletePalette
{
    public class DeletePaletteCommandHandler : IRequestHandler<DeletePaletteCommand, Result>
    {
        private readonly IColorPaletteRepository _paletteRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppLogger<DeletePaletteCommandHandler> _logger;

        public DeletePaletteCommandHandler(
            IColorPaletteRepository paletteRepository,
            ICurrentUserService currentUserService,
            IAppLogger<DeletePaletteCommandHandler> logger)
        {
            _paletteRepository = paletteRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<Result> Handle(DeletePaletteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var palette = await _paletteRepository.GetByIdAsync(request.Id);

                if (palette == null)
                    return Result.Failure($"Palette with ID {request.Id} not found.");

                // Check if the user has permission to delete this palette
                if (palette.UserId.HasValue && _currentUserService.IsAuthenticated &&
                    palette.UserId.Value != _currentUserService.UserId)
                {
                    return Result.Failure("You don't have permission to delete this palette.");
                }

                await _paletteRepository.DeleteAsync(palette);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting color palette");
                return Result.Failure($"Failed to delete palette: {ex.Message}");
            }
        }
    }
}
