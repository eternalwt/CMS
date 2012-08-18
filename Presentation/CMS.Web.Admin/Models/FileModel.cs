using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Admin.Models {
	public class FileModel {

		public string FileName { get; set; }

		public string FullName { get; set; }

		public string FilePath { get; set; }

		public string ThumbFilePath { get; set; }

		public bool IsImage { get; set; }
	}
}