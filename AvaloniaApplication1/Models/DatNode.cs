using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;

namespace AvaloniaApplication1.Models
{
    public class DatNode
    {
        public DatNode(string name, string description, DateTime dTime, string? imagePath = null, Color? backColor = null)
        {
            gName = name;
            gDescription = description;
            gDate = dTime;
            if (imagePath != null)
                gType = ImageLoader.Load(imagePath);
            if (backColor != null)
                bgColor = backColor.Value;
        }
        public Bitmap? gType { get; set; }

        public string gName { get; }

        public string gDescription { get; }

        public DateTime gDate { get; }


        public Color bgColor { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>
        /// <see cref="bgColor"/> as a brush, for binding to Background properties.
        /// </summary>
        public IBrush BgBrush => new SolidColorBrush(bgColor);
    }
}
