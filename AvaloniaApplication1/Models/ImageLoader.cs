using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;

namespace AvaloniaApplication1.Models
{
    /// <summary>
    /// Loads PNG (or any supported) images from either an
    /// "avares://" resource URI or a path on disk.
    /// </summary>
    public static class ImageLoader
    {
        public static Bitmap? Load(string path)
        {
            try
            {
                if (path.StartsWith("avares://", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = AssetLoader.Open(new Uri(path));
                    return new Bitmap(stream);
                }

                return File.Exists(path) ? new Bitmap(path) : null;
            }
            catch (Exception)
            {
                // A missing or corrupt image should not take the grid down.
                return null;
            }
        }

        public static IEnumerable<Bitmap> LoadAll(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                var bitmap = Load(path);
                if (bitmap is not null)
                {
                    yield return bitmap;
                }
            }
        }
    }
}
