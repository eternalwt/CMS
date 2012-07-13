using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data {
	public partial class Role : BaseEntity {

		public virtual int RoleID { get; set; }

		[Required(ErrorMessage = "Role name is required")]
		[StringLength(20, ErrorMessage = "Role name must be under 20 characters")]
		public virtual string RoleName { get; set; }

		private ICollection<ModulesInRoles> _modulesinroles;
		public virtual ICollection<ModulesInRoles> ModulesInRoles {
			get { return _modulesinroles ?? (_modulesinroles = new List<ModulesInRoles>()); }
			protected set { _modulesinroles = value; }
		}

		private ICollection<MenusInRoles> _menusinroles;
		public virtual ICollection<MenusInRoles> MenusInRoles {
			get { return _menusinroles ?? (_menusinroles = new List<MenusInRoles>()); }
			protected set { _menusinroles = value; }
		}

		private ICollection<User> _users;
		public virtual ICollection<User> Users {
			get { return _users ?? (_users = new List<User>()); }
			protected set { _users = value; }
		} 


	}
}
