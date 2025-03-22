using ChromaVision.Application.Common.Interfaces;
using ChromaVision.Application.Common.Mappings;
using ChromaVision.Application.Common.Models;
using ChromaVision.Application.Features.ColorPalettes.Commands.CreatePalette;
using ChromaVision.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChromaVision.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorPaletteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOpenAIService _openAIService;
        private readonly IImageProcessingService _imageService;

        public ColorPaletteController(
            IMediator mediator,
            IOpenAIService openAIService,
            IImageProcessingService imageService)
        {
            _mediator = mediator;
            _openAIService = openAIService;
            _imageService = imageService;
        }

        [HttpPost("generate-from-text")]
        public async Task<ActionResult<Result<ColorPaletteDto>>> GenerateFromText([FromBody] GenerateFromTextRequest request)
        {
            try
            {
                var colors = await _openAIService.GenerateColorsFromDescriptionAsync(
                    request.Description,
                    request.ColorCount);

                var command = new CreatePaletteCommand
                {
                    Name = request.Name ?? $"Generated from '{request.Description}'",
                    Description = request.Description,
                    Colors = colors
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result<ColorPaletteDto>.Failure($"An error occurred: {ex.Message}"));
            }
        }

        [HttpPost("generate-from-image")]
        public async Task<ActionResult<Result<ColorPaletteDto>>> GenerateFromImage([FromForm] GenerateFromImageRequest request)
        {
            try
            {
                if (request.Image == null || request.Image.Length == 0)
                {
                    return BadRequest(Result<ColorPaletteDto>.Failure("No image provided"));
                }

                using var stream = request.Image.OpenReadStream();
                var colors = await _imageService.ExtractColorsFromImageAsync(
                    stream,
                    request.ColorCount);

                var command = new CreatePaletteCommand
                {
                    Name = request.Name ?? $"Generated from {request.Image.FileName}",
                    Description = $"Colors extracted from image: {request.Image.FileName}",
                    Colors = colors
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result<ColorPaletteDto>.Failure($"An error occurred: {ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("my-palettes")]
        public async Task<ActionResult<Result<List<ColorPaletteDto>>>> GetMyPalettes()
        {
            try
            {
                // Use GetUserPalettesQuery via mediator
                return Ok(await Task.FromResult(new List<ColorPaletteDto>())); // Placeholder
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result<List<ColorPaletteDto>>.Failure($"An error occurred: {ex.Message}"));
            }
        }
    }

    public class GenerateFromTextRequest
    {
        public string Description { get; set; } = string.Empty;
        public string? Name { get; set; }
        public int ColorCount { get; set; } = 5;
    }

    public class GenerateFromImageRequest
    {
        public IFormFile Image { get; set; } = null!;
        public string? Name { get; set; }
        public int ColorCount { get; set; } = 5;
    }
}

