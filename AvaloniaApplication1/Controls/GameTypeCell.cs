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


            string bitmapNameDat = null;
            if (tRvDir.DatStatus != DatStatus.NotInDat && tRvDir.DatStatus != DatStatus.InToSort)
                bitmapNameDat = GetBitmapFromType(tRvDir.FileType, tRvDir.ZipDatStruct);

            string bitmapName = null;
            if (tRvDir.GotStatus != GotStatus.NotGot)
                bitmapName = GetBitmapFromType(tRvDir.FileType, tRvDir.ZipStruct);


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


            if (bm0 != -1)
            {
                Bitmap? bm = AssetsHelper.GetBitmap(bitmapNameDat);
                if (bm != null)
                {
                    double xSize = bm.Size.Width / bm.Size.Height * 18;
                    context.DrawImage(bm, new Rect(0, 0, bm.Size.Width, bm.Size.Height), new Rect((bm0 - (xSize / 2)), 5, xSize, 18));
                }
                else
                {
                    Debug.WriteLine("Missing Graphic for " + bitmapNameDat);
                }
            }

            if (bm1 != -1)
            {
                Bitmap? bm = AssetsHelper.GetBitmap(bitmapName);
                if (bm != null)
                {
                    double xSize = bm.Size.Width / bm.Size.Height * 18;
                    context.DrawImage(bm, new Rect(0, 0, bm.Size.Width, bm.Size.Height), new Rect((bm1 - (xSize / 2)), 5, xSize, 18));
                }
                else
                {
                    Debug.WriteLine("Missing Graphic for " + bitmapName);
                }
            }

            if (bm2 != -1)
            {
                Bitmap? bm = AssetsHelper.GetBitmap(tRvDir.ZipDatStructFix ? "ZipConvert" : "ZipConvert1");
                double xSize = bm.Size.Width / bm.Size.Height * 18;
                context.DrawImage(bm, new Rect(0, 0, bm.Size.Width, bm.Size.Height), new Rect((bm2 - (xSize / 2)), 5, xSize, 18));
            }
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
