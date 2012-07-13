using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class UserMap : EntityTypeConfiguration<User> {
		public UserMap() {
			this.HasKey(t => t.UserID);
			this.Property(m => m.Parameters).IsMaxLength();
			this.HasRequired(m => m.Role)
				.WithMany(m => m.Users)
				.HasForeignKey(m => m.RoleID)
				.WillCascadeOnDelete(false);
			this.ToTable("User");
		}
	}
}
