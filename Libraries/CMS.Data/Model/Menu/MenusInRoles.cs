using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class MenusInRoles {

			[Key, Column(Order = 1)]
			public int MenuID { get; set; }

			[Key, Column(Order = 2)]
			public int RoleID { get; set; }

			public Menu Menu { get; set; }

			public Role Role { get; set; }
		
	}
}
