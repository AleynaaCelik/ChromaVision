using AutoMapper;
using ChromaVision.Application.Features.ColorPalettes.Commands.CreatePalette;
using ChromaVision.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Color Palette mappings
            CreateMap<ColorPalette, ColorPaletteDto>()
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(c => c.HexCode).ToList()));

            CreateMap<CreatePaletteCommand, ColorPalette>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Colors, opt => opt.Ignore());

            // User mappings
            CreateMap<User, UserDto>();

            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
