using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class ModuleTypeMap : EntityTypeConfiguration<ModuleType> {
		public ModuleTypeMap() {
			this.HasKey(m => m.ModuleTypeID);
			this.ToTable("ModuleType");
		}
	}
}
