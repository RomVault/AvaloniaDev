using Avalonia.Media;
using AvaloniaApplication1.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using Compress;
using RomVaultCore;
using RomVaultCore.RvDB;
using RVIO;
using System;

namespace AvaloniaApplication1.Models
{
    public partial class GameNode : ObservableObject
    {

        public GameNode(RvFile file)
        {
            gFile = file;
        }

        public RvFile gFile { get; set; }


        public string gName
        {
            get
            {
                if (string.IsNullOrEmpty(gFile.FileName))
                    return gFile.Name;
                return $"{gFile.Name} (Found: {gFile.FileName})";
            }
        }

        public string gDescription
        {
            get
            {
                if (gFile.Game == null)
                    return "";
                string desc = gFile.Game.GetData(RvGame.GameData.Description);
                if (desc == "¤") desc = Path.GetFileNameWithoutExtension(gFile.Name);

                return desc;
            }
        }

        public string gDate
        {
            get
            {
                return SetCell(CompressUtils.zipDateTimeToString(gFile.FileModTimeStamp), gFile, FileStatus.DateFromDAT, 0, 0);
            }
        }

        public Color bgColor
        {
            get
            {
                ReportStatus tDirStat = gFile.DirStatus;

                foreach (RepStatus t1 in RepairStatus.DisplayOrder)
                {
                    if (tDirStat.Get(t1) <= 0)
                    {
                        continue;
                    }

                    return UIDisplayColors.IsDark(UIDisplayColors.DisplayColor(t1));
                }


                return Color.FromRgb(0, 0, 0);

            }
        }

        public Color fgColor
        {
            get
            {
                ReportStatus tDirStat = gFile.DirStatus;

                foreach (RepStatus t1 in RepairStatus.DisplayOrder)
                {
                    if (tDirStat.Get(t1) <= 0)
                    {
                        continue;
                    }

                    return UIDisplayColors.FontColor(t1);
                }


                return Color.FromRgb(255, 255, 255);

            }
        }

        public IBrush BgBrush => new SolidColorBrush(bgColor);
        public IBrush FgBrush => new SolidColorBrush(fgColor);



        private static string SetCell(string txt, RvFile tRomTable, FileStatus dat, FileStatus file, FileStatus verified)
        {
            string flags = "";
            if (dat != 0 && tRomTable.FileStatusIs(dat))
            {
                flags += "D";
            }

            if (file != 0 && tRomTable.FileStatusIs(file))
            {
                flags += "F";
            }

            if (verified != 0 && tRomTable.FileStatusIs(verified))
            {
                flags += "V";
            }

            if (!string.IsNullOrEmpty(flags))
            {
                flags = " (" + flags + ")";
            }

            return txt + flags;
        }
    }
}
