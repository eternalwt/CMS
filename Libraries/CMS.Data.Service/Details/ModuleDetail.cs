using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Data.Service.Details {
	public class ModuleDetail {

		public int ModuleID { get; set; }

		public string ModuleName { get; set; }

		public string ModuleTypeName { get; set; }

		public string PositionName { get; set; }

		public bool IsPublish { get; set; }

		public int SortOrder { get; set; }

		public string AccessLevelName { get; set; }

	}
}
