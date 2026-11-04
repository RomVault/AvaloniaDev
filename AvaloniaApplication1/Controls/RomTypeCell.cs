using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Helper;
using AvaloniaApplication1.Models;
using Compress.StructuredZip;
using RomVaultCore.RvDB;
using SkiaSharp;
using System.Diagnostics;

namespace AvaloniaApplication1.Controls
{
    /// <summary>
    /// Lightweight custom-drawn cell for the "Type" column. All drawing lives in
    /// <see cref="DrawCell"/> so it is easy to edit in one place. Bind its
    /// <see cref="Node"/> property to the row's <see cref="RomNode"/>.
    /// </summary>
    public class RomTypeCell : Control
    {
        public static readonly StyledProperty<RomNode?> NodeProperty =
            AvaloniaProperty.Register<RomTypeCell, RomNode?>(nameof(Node));

        public RomNode? Node
        {
            get => GetValue(NodeProperty);
            set => SetValue(NodeProperty, value);
        }

        static RomTypeCell()
        {
            // Repaint whenever the bound node changes.
            AffectsRender<RomTypeCell>(NodeProperty);
        }

        public override void Render(DrawingContext context) => DrawCell(context);

        /// <summary>
        /// *** Edit your custom drawing here. ***
        /// </summary>
        private void DrawCell(DrawingContext context)
        {
            RomNode? node = Node;
            if (node is null)
                return;
            RvFile tFile = node.tFile;

            Rect bounds = new(Bounds.Size);
            int width = (int)bounds.Width;

            string bitPlusMIA = "";
            if (tFile.DatStatus == DatStatus.InDatCollect || tFile.DatStatus == DatStatus.InDatMerged)
            {
                if (tFile.MIAStatusIs(MIAStatus.MIA)) bitPlusMIA = "_MIA";
                else if (tFile.MIAStatusIs(MIAStatus.MIAFromDat)) bitPlusMIA = "_MIA";
                else if (tFile.MIAStatusIs(MIAStatus.New)) bitPlusMIA = "_NEW";
            }

            string bitmapName = "R_" + tFile.DatStatus + "_" + tFile.RepStatus + bitPlusMIA;
            Bitmap? romIcon = AssetsHelper.GetBitmap(bitmapName);
            if (romIcon == null)
                return;

            context.DrawImage(romIcon, new Rect(0, 0, romIcon.Size.Width, romIcon.Size.Height),new Rect(4,bounds.Height/2-romIcon.Size.Height/2,romIcon.Size.Width,romIcon.Size.Height));
        }


    }
}
