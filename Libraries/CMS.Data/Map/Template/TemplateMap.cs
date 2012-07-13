using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using CMS.Data;

namespace CMS.Data.Map {
	public partial class TemplateMap : EntityTypeConfiguration<Template> {
		public TemplateMap() {
			this.HasKey(t => t.TemplateID);
			this.Property(t => t.AuthorName).IsOptional();
			this.Property(t => t.Version).IsOptional();
			this.ToTable("Template");
		}
	}
}
