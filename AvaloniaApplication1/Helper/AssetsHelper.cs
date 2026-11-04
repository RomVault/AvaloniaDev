using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AvaloniaApplication1.Helper
{
    internal static class AssetsHelper
    {
        private const string AssetsRoot = "avares://AvaloniaApplication1/Assets/";

        /// <summary>
        /// Cache of already-loaded asset bitmaps, keyed by the png file name.
        /// </summary>
        private static readonly Dictionary<string, Bitmap?> _cache =
            new(StringComparer.OrdinalIgnoreCase);

        public static Bitmap? GetBitmap(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            if (_cache.TryGetValue(fileName, out Bitmap? cached))
                return cached;

            Bitmap? bitmap = null;
            try
            {
                var uri = new Uri(AssetsRoot + fileName+".png");
                using var stream = AssetLoader.Open(uri);
                bitmap = new Bitmap(stream);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Image not found {fileName}");
                _cache[fileName] = null;
                return null;
            }

            _cache[fileName] = bitmap;
            return bitmap;
        }
    }
}
