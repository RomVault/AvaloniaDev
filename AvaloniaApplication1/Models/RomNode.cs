using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Helper;
using RomVaultCore;
using RomVaultCore.RvDB;
using RVUtils;
using System;

namespace AvaloniaApplication1.Models
{
    /// <summary>
    /// A single ROM row in the RomGrid.
    /// </summary>
    public class RomNode
    {
        public RomNode(RvFile rom)
        {
            tFile = rom;
        }

        public RvFile tFile { get; set; }

        public Bitmap? rGot { get; set; }

        public string rName
        {
            get
            {
                string fname = tFile.UiDisplayName;
                if (!string.IsNullOrEmpty(tFile.FileName))
                {
                    fname += " (Found: " + tFile.FileName + ")";
                }

                if (tFile.CHDVersion != null)
                {
                    fname += " (V" + tFile.CHDVersion + ")";
                }

                string D = tFile.FileStatusIs(FileStatus.HeaderFileTypeFromDAT) ? "D" : "";
                string F = tFile.FileStatusIs(FileStatus.HeaderFileTypeFromHeader) ? "F" : "";
                if (tFile.HeaderFileType != HeaderFileType.Nothing || !string.IsNullOrWhiteSpace(D) || !string.IsNullOrWhiteSpace(F))
                {
                    string req = tFile.HeaderFileTypeRequired ? ",Required" : "";
                    fname += $" ({tFile.HeaderFileType}{req} {D}{F})";
                }

                return fname;
            }
        }

        public string rMerge { get { return tFile.Merge; } }

        public string rSize { get { return SetCell(tFile.Size == null ? "" : ((ulong)tFile.Size).ToRvString(), tFile, FileStatus.SizeFromDAT, FileStatus.SizeFromHeader, FileStatus.SizeVerified); } }

        public string rCRC32 { get { return SetCell(tFile.CRC.ToHexString(), tFile, FileStatus.CRCFromDAT, FileStatus.CRCFromHeader, FileStatus.CRCVerified); } }

        public string rSHA1 { get { return SetCell(tFile.SHA1.ToHexString(), tFile, FileStatus.SHA1FromDAT, FileStatus.SHA1FromHeader, FileStatus.SHA1Verified); } }

        public string rMD5 { get { return SetCell(tFile.MD5.ToHexString(), tFile, FileStatus.MD5FromDAT, FileStatus.MD5FromHeader, FileStatus.MD5Verified); } }

        public string rAltSize { get { return SetCell(tFile.AltSize == null ? "" : ((ulong)tFile.AltSize).ToString("N0"), tFile, FileStatus.AltSizeFromDAT, FileStatus.AltSizeFromHeader, FileStatus.AltSizeVerified); } }

        public string rAltCRC32 { get { return SetCell(tFile.AltCRC.ToHexString(), tFile, FileStatus.AltCRCFromDAT, FileStatus.AltCRCFromHeader, FileStatus.AltCRCVerified); } }

        public string rAltSHA1 { get { return SetCell(tFile.AltSHA1.ToHexString(), tFile, FileStatus.AltSHA1FromDAT, FileStatus.AltSHA1FromHeader, FileStatus.AltSHA1Verified); } }

        public string rAltMD5 { get { return SetCell(tFile.AltMD5.ToHexString(), tFile, FileStatus.AltMD5FromDAT, FileStatus.AltMD5FromHeader, FileStatus.AltMD5Verified); } }

        public string rStatus { get { return tFile.Status; } }

        public DateTime? rFileModDate { get; set; }

        public int? rZipIndex { get; set; }

        public int? rInstanceCount { get; set; }

        public Color bgColor { get {
                return UIDisplayColors.IsDark(UIDisplayColors.DisplayColor(ReportStatus.UIStatus(tFile.MIAStatus, tFile.RepStatus))); } }

        public Color fgColor { get { 
                return UIDisplayColors.FontColor(tFile.RepStatus); } }

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
