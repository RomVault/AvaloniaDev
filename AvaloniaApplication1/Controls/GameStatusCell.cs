using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Helper;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels;
using RomVaultCore;
using RomVaultCore.RvDB;
using System.Diagnostics;
using RVUtils;

namespace AvaloniaApplication1.Controls
{
    public class GameStatusCell : Control
    {
        public static readonly StyledProperty<GameNode?> NodeProperty =
            AvaloniaProperty.Register<GameStatusCell, GameNode?>(nameof(Node));

        public GameNode? Node
        {
            get => GetValue(NodeProperty);
            set => SetValue(NodeProperty, value);
        }

        static GameStatusCell()
        {
            // Repaint whenever the bound node changes.
            AffectsRender<GameStatusCell>(NodeProperty);
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

            //g.Clear(Dark.dark.bgColor1(Color.White));

            //Font drawFont = new Font("Arial", 9);

            int gOff;
            int columnIndex = 0;
            for (int l = 0; l < RepairStatus.DisplayOrder.Length; l++)
            {
                if (l >= 13)
                {
                    columnIndex = l;
                }

                if (tRvDir.DirStatus.Get(RepairStatus.DisplayOrder[l]) <= 0)
                {
                    continue;
                }

                gOff = GameGridViewModel._gameGridColumnXPositions[columnIndex];
                Bitmap? bmg = AssetsHelper.GetBitmap(@"G_" + RepairStatus.DisplayOrder[l]);
                if (bmg != null)
                {
                    context.DrawImage(bmg, new Rect(gOff, 6, 21, 18));
                }
                else
                {
                    Debug.WriteLine("Missing Graphics for " + "G_" + RepairStatus.DisplayOrder[l]);
                }

                columnIndex++;
            }

            columnIndex = 0;
            for (int l = 0; l < RepairStatus.DisplayOrder.Length; l++)
            {
                if (l >= 13)
                {
                    columnIndex = l;
                }

                if (tRvDir.DirStatus.Get(RepairStatus.DisplayOrder[l]) > 0)
                {
                    gOff = GameGridViewModel._gameGridColumnXPositions[columnIndex];
                    string count = tRvDir.DirStatus.Get(RepairStatus.DisplayOrder[l]).ToRvString();

                    var text = new FormattedText(
                        count,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        12,
                        Avalonia.Media.Brushes.Black);
                    context.DrawText(text, new Avalonia.Point(gOff + 22, 8));
                    columnIndex++;
                }
            }

        }
    }
}
