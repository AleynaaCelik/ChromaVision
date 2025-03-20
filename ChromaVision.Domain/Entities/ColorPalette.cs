using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Domain.Entities
{
    public class ColorPalette
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty; // Default değer
        public string Description { get; private set; } = string.Empty; // Default değer
        public IReadOnlyList<ColorItem> Colors => _colors.AsReadOnly();
        public Guid? UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private List<ColorItem> _colors = new List<ColorItem>();

        protected ColorPalette() { } // For EF Core

        public ColorPalette(string? name, string? description, List<ColorItem>? colors, Guid? userId = null)
        {
            Id = Guid.NewGuid();
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            _colors = colors ?? new List<ColorItem>();
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddColor(ColorItem color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            _colors.Add(color);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveColor(ColorItem color)
        {
            _colors.Remove(color);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateColors(List<ColorItem> colors)
        {
            if (colors == null)
                throw new ArgumentNullException(nameof(colors));

            _colors = colors;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
