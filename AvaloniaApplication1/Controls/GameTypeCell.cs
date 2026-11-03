using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
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
    /// <see cref="Node"/> property to the row's <see cref="GameNode"/>.
    /// </summary>
    public class GameTypeCell : Control
    {
        public static readonly StyledProperty<GameNode?> NodeProperty =
            AvaloniaProperty.Register<GameTypeCell, GameNode?>(nameof(Node));

        public GameNode? Node
        {
            get => GetValue(NodeProperty);
            set => SetValue(NodeProperty, value);
        }

        static GameTypeCell()
        {
            // Repaint whenever the bound node changes.
            AffectsRender<GameTypeCell>(NodeProperty);
        }

        public override void Render(DrawingContext context) => DrawCell(context);

        /// <summary>
        /// *** Edit your custom drawing here. ***
        /// </summary>
        private void DrawCell(DrawingContext context)
        {
            GameNode? node = Node;
            if (node is null)
                return;
            RvFile tRvDir = node.gFile;
            
            Rect bounds = new(Bounds.Size);
            int width = (int)bounds.Width;
            /*
            // Optional background fill from the node.
            if (node.bgColor.A != 0)
                context.FillRectangle(new SolidColorBrush(node.bgColor), bounds);

            // Draw the type string, vertically centered, with a small left pad.
            string type = node.gFile?.Name ?? string.Empty;

            var formatted = new FormattedText(
                type,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface.Default,
                12,
                node.FgBrush);

            double x = 4;
            double y = (bounds.Height - formatted.Height) / 2;
            context.DrawText(formatted, new Point(x, y));

            // Add your own overlays here, e.g.:
            // context.DrawEllipse(Brushes.Red, null, new Point(bounds.Width - 5, 5), 3, 3);
            */


            string bitmapNameDat = null;
            if (tRvDir.DatStatus != DatStatus.NotInDat && tRvDir.DatStatus != DatStatus.InToSort)
                bitmapNameDat = GetBitmapFromType(tRvDir.FileType, tRvDir.ZipDatStruct);

            string bitmapName = null;
            if (tRvDir.GotStatus != GotStatus.NotGot)
                bitmapName = GetBitmapFromType(tRvDir.FileType, tRvDir.ZipStruct);

            //Bitmap bmp = new Bitmap(GameGrid.Columns[(int)GameGridColumns.CType].Width, 18);

            int bm0 = -1;
            int bm1 = -1;
            int bm2 = -1;
            if (bitmapNameDat != null && bitmapName != null)
            {
                if (bitmapNameDat == bitmapName)
                {
                    bm0 = (width / 2);
                }
                else
                {
                    bitmapNameDat += "Missing";
                    bm0 = (width / 4) * 3;
                    bm1 = (width / 4);
                    bm2 = (width / 2);
                }
            }
            else if (bitmapNameDat != null)
            {
                bm0 = (width / 2);
                bitmapNameDat += "Missing";
            }
            else if (bitmapName != null)
            {
                bm1 = (width / 2);
            }

            if (tRvDir.GotStatus == GotStatus.Corrupt) { bitmapName += "Corrupt"; }


            var formatted = new FormattedText(
               bitmapNameDat??"",
               System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight,
               Typeface.Default,
               12,
               node.FgBrush);

            double x = 4;
            double y = (bounds.Height - formatted.Height) / 2;
            context.DrawText(formatted, new Point(x, y));

            /*
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                if (bm0 != -1)
                {
                    Bitmap bm = rvImages.GetBitmap(bitmapNameDat, false);
                    if (bm != null)
                    {
                        float xSize = (float)bm.Width / bm.Height * 18;
                        g.DrawImage(bm, (bm0 - (xSize / 2)), 0, xSize, 18);
                    }
                    else
                    {
                        Debug.WriteLine("Missing Graphic for " + bitmapNameDat);
                    }
                }

                if (bm1 != -1)
                {
                    Bitmap bm = rvImages.GetBitmap(bitmapName, false);
                    if (bm != null)
                    {
                        float xSize = (float)bm.Width / bm.Height * 18;
                        g.DrawImage(bm, (bm1 - (xSize / 2)), 0, xSize, 18);
                    }
                    else
                    {
                        Debug.WriteLine("Missing Graphic for " + bitmapName);
                    }
                }

                if (bm2 != -1)
                {
                    Bitmap bm = rvImages.GetBitmap(tRvDir.ZipDatStructFix ? "ZipConvert" : "ZipConvert1", false);
                    float xSize = (float)bm.Width / bm.Height * 18;
                    g.DrawImage(bm, (bm2 - (xSize / 2)), 0, xSize, 18);
                }
            }*/

            //e.Value = bmp;

        }


        private static string GetBitmapFromType(FileType ft, ZipStructure zs)
        {
            switch (ft)
            {
                case FileType.Zip:
                    if (zs == ZipStructure.None) { return "Zip"; }
                    if (zs == ZipStructure.ZipTrrnt) { return "ZipTrrnt"; }
                    if (zs == ZipStructure.ZipTDC) { return "ZipTDC"; }
                    if (zs == ZipStructure.ZipZSTD) { return "ZipZSTD"; }
                    if (zs == ZipStructure.ZipDTD) { return "ZipDTD"; }
                    if (zs == ZipStructure.ZipDTZ) { return "ZipDTZ"; }
                    return null;
                case FileType.SevenZip:
                    if (zs == ZipStructure.None) { return "SevenZip"; }
                    if (zs == ZipStructure.SevenZipTrrnt) { return "SevenZipTrrnt"; }
                    if (zs == ZipStructure.SevenZipSLZMA) { return "SevenZipSLZMA"; }
                    if (zs == ZipStructure.SevenZipNLZMA) { return "SevenZipNLZMA"; }
                    if (zs == ZipStructure.SevenZipSZSTD) { return "SevenZipSZSTD"; }
                    if (zs == ZipStructure.SevenZipNZSTD) { return "SevenZipNZSTD"; }
                    return null;
                case FileType.Dir:
                    return "Dir";
            }
            return null;
        }
    }
}
