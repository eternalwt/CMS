using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {

	public partial class ModulesInMenus {

		[Key, Column(Order = 1)]
		public int ModuleID { get; set; }

		[Key, Column(Order = 2)]
		public int MenuID { get; set; }

		public Module Module { get; set; }

		public Menu Menu { get; set; }

	}
}
