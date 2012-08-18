using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Admin.Models {
	public class DirectoryModal {
		public DirectoryModal() {
			Files = new List<FileModel>();
			Directories = new List<DirectoryModal>();
		}

		public string DirectoryName { get; set; }

		public string DirectoryFullName { get; set; }

		public List<FileModel> Files { get; set; }

		public List<DirectoryModal> Directories { get; set; }
	}
}