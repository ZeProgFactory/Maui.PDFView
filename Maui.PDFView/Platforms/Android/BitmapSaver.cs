#if ANDROID
using Android.Graphics;
using System;
using System.IO;
using Microsoft.Maui.Storage;

public class BitmapSaver
{
   /// <summary>
   /// Saves an Android.Graphics.Bitmap to a file in PNG or JPEG format.
   /// </summary>
   /// <param name="bitmap">The bitmap to save.</param>
   /// <param name="fileName">The file name (without path).</param>
   /// <param name="format">The image format (PNG or JPEG).</param>
   /// <param name="quality">JPEG quality (0-100). Ignored for PNG.</param>
   /// <returns>The full file path where the bitmap was saved.</returns>
   public static string SaveBitmapToFile(Bitmap bitmap, string fileName, Bitmap.CompressFormat format, int quality = 100 )
   {
      if (bitmap == null)
         throw new ArgumentNullException(nameof(bitmap));

      if (string.IsNullOrWhiteSpace(fileName))
         throw new ArgumentException("File name cannot be empty.", nameof(fileName));

      // Ensure file name has correct extension
      //string extension = format == Bitmap.CompressFormat.Png ? ".png" : ".jpg";
      //if (!fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
      //   fileName += extension;

      //// Save to app's cache directory
      //string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
      string filePath = fileName;

      try
      {
         using (var stream = new FileStream(filePath, FileMode.Create))
         {
            // Compress and write bitmap to file
            bool success = bitmap.Compress(format, quality, stream);
            if (!success)
               throw new IOException("Failed to compress bitmap.");
         }
      }
      catch (Exception ex)
      {
         throw new IOException($"Error saving bitmap to file: {ex.Message}", ex);
      }

      return filePath;
   }
}
#endif
