using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class MenusInRolesMap : EntityTypeConfiguration<MenusInRoles> {
		public MenusInRolesMap() {
			this.HasRequired(m => m.Menu)
					.WithMany(m => m.MenusInRoles)
					.HasForeignKey(m => m.MenuID)
					.WillCascadeOnDelete(true);
			this.HasRequired(m => m.Role)
					.WithMany(m => m.MenusInRoles)
					.HasForeignKey(m => m.RoleID)
					.WillCascadeOnDelete(true);
		}
	}
}
