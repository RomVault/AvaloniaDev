using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;

namespace AvaloniaApplication1.Models
{
    /// <summary>
    /// A single ROM row in the RomGrid.
    /// </summary>
    public class RomNode
    {
        public RomNode(string rom, string? imagePath = null, Color? backColor = null)
        {
            rRom = rom;
            if (imagePath != null)
                rGot = ImageLoader.Load(imagePath);
            if (backColor != null)
                bgColor = backColor.Value;
        }

        /// <summary>
        /// "Got" status icon, shown in the first column.
        /// </summary>
        public Bitmap? rGot { get; set; }

        public string rRom { get; set; } = "";

        public string rMerge { get; set; } = "";

        public ulong? rSize { get; set; }

        public string rCRC32 { get; set; } = "";

        public string rSHA1 { get; set; } = "";

        public string rMD5 { get; set; } = "";

        public ulong? rAltSize { get; set; }

        public string rAltCRC32 { get; set; } = "";

        public string rAltSHA1 { get; set; } = "";

        public string rAltMD5 { get; set; } = "";

        public string rStatus { get; set; } = "";

        public DateTime? rFileModDate { get; set; }

        public int? rZipIndex { get; set; }

        public int? rInstanceCount { get; set; }

        public Color bgColor { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>
        /// <see cref="bgColor"/> as a brush, for binding to Background properties.
        /// </summary>
        public IBrush BgBrush => new SolidColorBrush(bgColor);
    }
}
