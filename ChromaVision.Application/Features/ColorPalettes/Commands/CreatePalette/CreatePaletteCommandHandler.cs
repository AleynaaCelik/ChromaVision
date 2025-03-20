using AutoMapper;
using ChromaVision.Application.Common.Mappings;
using ChromaVision.Core.Interfaces;
using ChromaVision.Core.Models;
using ChromaVision.Domain.Entities;
using ChromaVision.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.ColorPalettes.Commands.CreatePalette
{
    public class CreatePaletteCommandHandler : IRequestHandler<CreatePaletteCommand, Result<ColorPaletteDto>>
    {
        private readonly IColorPaletteRepository _paletteRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreatePaletteCommandHandler> _logger;

        public CreatePaletteCommandHandler(
            IColorPaletteRepository paletteRepository,
            ICurrentUserService currentUserService,
            IMapper mapper,
            IAppLogger<CreatePaletteCommandHandler> logger)
        {
            _paletteRepository = paletteRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ColorPaletteDto>> Handle(CreatePaletteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Use the current user ID if authenticated and no UserID provided
                if (request.UserId == null && _currentUserService.IsAuthenticated)
                {
                    request.UserId = _currentUserService.UserId;
                }

                // Create color items from the hex codes
                var colorItems = request.Colors
                    .Select((hexCode, index) => new ColorItem(hexCode, string.Empty, index))
                    .ToList();

                // Create the palette entity
                var palette = new ColorPalette(
                    request.Name,
                    request.Description,
                    colorItems,
                    request.UserId
                );

                // Save to database
                await _paletteRepository.AddAsync(palette);

                // Map to DTO and return
                var paletteDto = _mapper.Map<ColorPaletteDto>(palette);
                return Result<ColorPaletteDto>.Success(paletteDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating color palette");
                return Result<ColorPaletteDto>.Failure($"Failed to create palette: {ex.Message}");
            }
        }
    }
}
