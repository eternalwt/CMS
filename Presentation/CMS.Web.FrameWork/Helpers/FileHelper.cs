using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace CMS.Web.FrameWork.Helpers {
	public static class FileHelper {

		//Return thumbnail callback
		public static bool ThumbnailCallback() {
			return true;
		}

		//For image thumbnial
		public static void CreateThumbNail(string sourceFileName, string destinationFileName, int width, int height) {
			try {
				Image thumb;
				Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
				if (System.IO.File.Exists(sourceFileName)) {
					string imgname = System.IO.Path.GetFileName(sourceFileName);
					string extension = System.IO.Path.GetExtension(sourceFileName).ToLower();
					ImageFormat imageFormat = null;
					switch (extension) {
						case ".jpg":
							imageFormat = ImageFormat.Jpeg;
							break;
						case ".jpeg":
							imageFormat = ImageFormat.Jpeg;
							break;
						case ".bmp":
							imageFormat = ImageFormat.Bmp;
							break;
						case ".emf":
							imageFormat = ImageFormat.Emf;
							break;
						case ".exif":
							imageFormat = ImageFormat.Exif;
							break;
						case ".gif":
							imageFormat = ImageFormat.Gif;
							break;
						case ".ico":
							imageFormat = ImageFormat.Icon;
							break;
						case ".png":
							imageFormat = ImageFormat.Png;
							break;
						case ".tiff":
							imageFormat = ImageFormat.Tiff;
							break;
						case ".wmf":
							imageFormat = ImageFormat.Wmf;
							break;
					}
					if (imageFormat != null) {
						System.Drawing.Image imagesize = System.Drawing.Image.FromFile(sourceFileName);
						Bitmap bitmapNew = new Bitmap(imagesize);
						thumb = bitmapNew.GetThumbnailImage(width, height, null, IntPtr.Zero);
						//Save image in TumbnailImage folder
						thumb.Save(destinationFileName, imageFormat);
					}
				}
			} catch { }
		}

	}
}
