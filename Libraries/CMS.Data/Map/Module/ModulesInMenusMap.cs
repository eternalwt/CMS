using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class ModulesInMenusMap : EntityTypeConfiguration<ModulesInMenus> {
		public ModulesInMenusMap() {
			this.HasRequired(m => m.Module)
					.WithMany(m => m.ModulesInMenus)
					.HasForeignKey(m => m.ModuleID)
					.WillCascadeOnDelete(true);
			this.HasRequired(m => m.Menu)
					.WithMany(m => m.ModulesInMenus)
					.HasForeignKey(m => m.MenuID)
					.WillCascadeOnDelete(true);
		}
	}
}
