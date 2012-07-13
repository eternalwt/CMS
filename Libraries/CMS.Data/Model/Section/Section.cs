using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class Section : BaseEntity {

		public virtual int SectionID { get; set; }

		[Required(ErrorMessage = "Section name is required")]
		[StringLength(50, ErrorMessage = "Section name must be under 50 characters")]
		public virtual string SectionName { get; set; }

		[Required(ErrorMessage = "IsPublished is required")]
		public virtual bool IsPublished { get; set; }

		[Required(ErrorMessage = "Sort order is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Sort order is required")]
		public virtual int SortOrder { get; set; }

		public virtual string Parameters { get; set; }

		private ICollection<Category> _categories;
		public virtual ICollection<Category> Categories {
			get { return _categories ?? (_categories = new List<Category>()); }
			protected set { _categories = value; }
		}

	}
}
