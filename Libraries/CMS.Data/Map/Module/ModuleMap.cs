using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class ModuleMap : EntityTypeConfiguration<Module> {
		public ModuleMap(){
			this.HasKey(m => m.ModuleID);
			this.Property(m => m.Parameters).IsMaxLength();
			this.HasRequired(m => m.AccessLevel)
				.WithMany(m => m.Modules)
				.HasForeignKey(m => m.AccessLevelID)
				.WillCascadeOnDelete(false);
			this.HasRequired(m => m.ModuleType)
				.WithMany(m => m.Modules)
				.HasForeignKey(m => m.ModuleTypeID)
				.WillCascadeOnDelete(false);
			this.HasRequired(m => m.Position)
				.WithMany(m => m.Modules)
				.HasForeignKey(m => m.PositionID)
				.WillCascadeOnDelete(false);
			this.ToTable("Module");
		}
	}
}
