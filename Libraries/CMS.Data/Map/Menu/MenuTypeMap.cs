using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class MenuTypeMap : EntityTypeConfiguration<MenuType> {
		public MenuTypeMap() {
			this.HasKey(m => m.MenuTypeID);
			this.ToTable("MenuType");
		}
	}
}
