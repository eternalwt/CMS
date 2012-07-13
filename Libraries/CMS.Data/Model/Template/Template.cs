using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data {
	public partial class Template : BaseEntity {

		public virtual int TemplateID { get; set; }

		[Required(ErrorMessage = "Template Name is required")]
		[StringLength(50, ErrorMessage = "Template Name must be under 50 characters")]
		public virtual string Name { get; set; }

		[StringLength(50, ErrorMessage = "Author Name must be under 50 characters")]
		public string AuthorName { get; set; }

		[StringLength(10, ErrorMessage = "Version must be under 10 characters")]
		public string Version { get; set; }

		public virtual string Parameters { get; set; }

		public virtual bool IsBackEnd { get; set; }

		public virtual bool IsDefault { get; set; }

		public virtual bool IsEnable { get; set; }

	}
}
