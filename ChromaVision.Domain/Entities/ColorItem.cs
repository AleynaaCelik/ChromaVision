using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Domain.Entities
{
    public class ColorItem
    {
        public Guid Id { get; private set; }
        public string HexCode { get; private set; } = string.Empty; // Default değer
        public string Name { get; private set; } = string.Empty; // Default değer
        public int OrderIndex { get; private set; }
        public Guid PaletteId { get; private set; }

        protected ColorItem() { } // For EF Core

        public ColorItem(string hexCode, string? name = null, int orderIndex = 0)
        {
            Id = Guid.NewGuid();
            HexCode = string.IsNullOrWhiteSpace(hexCode) ? "#000000" : NormalizeHexCode(hexCode);
            Name = name ?? string.Empty;
            OrderIndex = orderIndex;
        }


        public void UpdateHexCode(string hexCode)
        {
            if (string.IsNullOrWhiteSpace(hexCode))
                throw new ArgumentException("Hex code cannot be empty", nameof(hexCode));

            HexCode = NormalizeHexCode(hexCode);
        }

        public void UpdateName(string name)
        {
            Name = name ?? string.Empty;
        }

        public void UpdateOrderIndex(int orderIndex)
        {
            OrderIndex = orderIndex;
        }

        private string NormalizeHexCode(string hexCode)
        {
            // Ensure hex code is valid and properly formatted
            hexCode = hexCode.Trim();

            // Add # if missing
            if (!hexCode.StartsWith("#"))
                hexCode = "#" + hexCode;

            // Ensure correct length
            if (hexCode.Length == 4) // #RGB format
            {
                // Convert #RGB to #RRGGBB
                hexCode = "#" + hexCode[1] + hexCode[1] + hexCode[2] + hexCode[2] + hexCode[3] + hexCode[3];
            }

            return hexCode.ToUpper();
        }
    }
}
