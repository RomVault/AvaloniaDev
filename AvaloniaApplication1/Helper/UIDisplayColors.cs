using RomVaultCore;
using Avalonia.Media;

namespace AvaloniaApplication1.Helper
{
    public static class UIDisplayColors
    {

        private static readonly Color CBlue = Color.FromRgb(214, 214, 255);
        private static readonly Color CGreyBlue = Color.FromRgb(214, 224, 255);
        private static readonly Color CRed = Color.FromRgb(255, 214, 214);
        private static readonly Color CBrightRed = Color.FromRgb(255, 0, 0);
        private static readonly Color CGreen = Color.FromRgb(214, 255, 214);
        private static readonly Color CNeonGreen = Color.FromRgb(100, 255, 100);
        private static readonly Color CLightRed = Color.FromRgb(255, 235, 235);
        private static readonly Color CSoftGreen = Color.FromRgb(150, 200, 150);
        private static readonly Color CGrey = Color.FromRgb(214, 214, 214);
        private static readonly Color CCyan = Color.FromRgb(214, 255, 255);
        private static readonly Color CCyanGrey = Color.FromRgb(214, 225, 225);
        private static readonly Color CMagenta = Color.FromRgb(255, 214, 255);
        private static readonly Color CBrown = Color.FromRgb(140, 80, 80);
        private static readonly Color CPurple = Color.FromRgb(214, 140, 214);
        private static readonly Color CYellow = Color.FromRgb(255, 255, 214);
        private static readonly Color CDarkYellow = Color.FromRgb(255, 255, 100);
        private static readonly Color COrange = Color.FromRgb(255, 214, 140);
        private static readonly Color CWhite = Color.FromRgb(255, 255, 255);
        private static readonly Color[] _displayColor;
        private static readonly Color[] _fontColor;

        static UIDisplayColors()
        {
            _displayColor = new Color[(int)RepStatus.EndValue];
            _fontColor = new Color[(int)RepStatus.EndValue];

            // RepStatus.UnSet

            _displayColor[(int)RepStatus.UnScanned] = CBlue;

            _displayColor[(int)RepStatus.DirCorrect] = CGreen;
            _displayColor[(int)RepStatus.DirMissing] = CRed;
            _displayColor[(int)RepStatus.DirCorrupt] = CBrightRed; //BrightRed

            _displayColor[(int)RepStatus.Missing] = CRed;
            _displayColor[(int)RepStatus.MissingNew] = CSoftGreen;
            _displayColor[(int)RepStatus.Correct] = CGreen;
            _displayColor[(int)RepStatus.CorrectMIA] = CNeonGreen;
            _displayColor[(int)RepStatus.NotCollected] = CGrey;
            _displayColor[(int)RepStatus.UnNeeded] = CCyanGrey;
            _displayColor[(int)RepStatus.Unknown] = CCyan;
            _displayColor[(int)RepStatus.InToSort] = CMagenta;

            _displayColor[(int)RepStatus.MissingMIA] = CSoftGreen;

            _displayColor[(int)RepStatus.Corrupt] = CBrightRed; //BrightRed
            _displayColor[(int)RepStatus.Ignore] = CGreyBlue;

            _displayColor[(int)RepStatus.CanBeFixed] = CYellow;
            _displayColor[(int)RepStatus.CanBeFixedMIA] = CDarkYellow;
            _displayColor[(int)RepStatus.MoveToSort] = CPurple;
            _displayColor[(int)RepStatus.Delete] = CBrown;
            _displayColor[(int)RepStatus.NeededForFix] = COrange;
            _displayColor[(int)RepStatus.Rename] = COrange;

            _displayColor[(int)RepStatus.CorruptCanBeFixed] = CYellow;
            _displayColor[(int)RepStatus.MoveToCorrupt] = CPurple; //Missing

            _displayColor[(int)RepStatus.Incomplete] = CLightRed;

            _displayColor[(int)RepStatus.Deleted] = CWhite;

            for (int i = 0; i < (int)RepStatus.EndValue; i++)
            {
                _fontColor[i] = Contrasty(_displayColor[i]);
            }

        }

        public static Color DisplayColor(RepStatus status)
        {
            return _displayColor[(int)status];
        }

        public static Color FontColor(RepStatus status)
        {
            return _fontColor[(int)status];
        }

        public static Color IsDark(Color c)
        {
            return Color.FromRgb((byte)(c.R * 0.7), (byte)(c.G * 0.7), (byte)(c.B * 0.7));
        }

        // returns either white or black, depending of quick luminance of the Color " a "
        // called when the _displayColor is finished, in order to populate the _fontColor table.
        private static Color Contrasty(Color a)
        {
            return (a.R << 1) + a.B + a.G + (a.G << 2) < 1024 ? Color.FromRgb(255,255,255) : Color.FromRgb(0,0,0);
        }

    }
}
