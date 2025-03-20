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
        public string HexCode { get; private set; }
        public string Name { get; private set; }
        public int OrderIndex { get; private set; }
        public Guid PaletteId { get; private set; }

        protected ColorItem() { } // For EF Core

        // ChromaVision.Domain/Entities/ColorItem.cs - HexCode için null kontrolü
        public ColorItem(string hexCode, string? name = null, int orderIndex = 0)
        {
            if (string.IsNullOrWhiteSpace(hexCode))
                throw new ArgumentException("Hex code cannot be empty", nameof(hexCode));

            Id = Guid.NewGuid();
            HexCode = NormalizeHexCode(hexCode);
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
