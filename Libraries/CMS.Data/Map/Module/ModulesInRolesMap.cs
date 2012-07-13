using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class ModulesInRolesMap : EntityTypeConfiguration<ModulesInRoles> {
		public ModulesInRolesMap() {
			this.HasRequired(m => m.Module)
					.WithMany(m => m.ModulesInRoles)
					.HasForeignKey(m => m.ModuleID)
					.WillCascadeOnDelete(true);
			this.HasRequired(m => m.Role)
					.WithMany(m => m.ModulesInRoles)
					.HasForeignKey(m => m.RoleID)
					.WillCascadeOnDelete(true);
		}
	}
}
