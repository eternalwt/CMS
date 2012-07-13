using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class MenuMap : EntityTypeConfiguration<Menu> {
		public MenuMap(){
			this.HasKey(m => m.MenuID);
			this.Property(m => m.Parameters).IsMaxLength();
			this.HasRequired(m => m.AccessLevel)
				.WithMany(m => m.Menus)
				.HasForeignKey(m => m.AccessLevelID)
				.WillCascadeOnDelete(false);
			this.HasRequired(m => m.MenuType)
				.WithMany(m => m.Menus)
				.HasForeignKey(m => m.MenuTypeID)
				.WillCascadeOnDelete(false);
			this.HasOptional(m => m.ParentMenu)
				.WithMany(m => m.ChildMenus)
				.HasForeignKey(m => m.ParentID)
				.WillCascadeOnDelete(false);
			this.ToTable("Menu");
		}
	}
}
