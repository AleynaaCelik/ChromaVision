using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Features.ColorPalettes.Commands
{
    public class CreatePaletteCommandValidator : AbstractValidator<CreatePaletteCommand>
    {
        public CreatePaletteCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(v => v.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(v => v.Colors)
                .NotEmpty().WithMessage("At least one color is required")
                .Must(colors => colors.Count <= 10).WithMessage("Cannot have more than 10 colors");

            RuleForEach(v => v.Colors)
                .NotEmpty().WithMessage("Color hex code cannot be empty")
                .Matches("^#?([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$").WithMessage("Invalid hex color format");
        }
    }
}
