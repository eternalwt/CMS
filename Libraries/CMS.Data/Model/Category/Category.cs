using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class Category : BaseEntity {

		public virtual int CategoryID { get; set; }

		[Required(ErrorMessage = "Category name is required")]
		[StringLength(50, ErrorMessage = "Category name must be under 50 characters")]
		public virtual string CategoryName { get; set; }

		[Required(ErrorMessage = "Section is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Section is required")]
		public virtual int SectionID { get; set; }

		[Required(ErrorMessage = "IsPublished is required")]
		public virtual bool IsPublished { get; set; }

		[Required(ErrorMessage = "Sort order is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Sort order is required")]
		public virtual int SortOrder { get; set; }

		public virtual string Parameters { get; set; }

		public virtual int? ParentID { get; set; }

		public virtual Category ParentCategory { get; set; }

		public virtual Section Section { get; set; }

		private ICollection<Category> _childcategories;
		public virtual ICollection<Category> ChildCategories {
			get { return _childcategories ?? (_childcategories = new List<Category>()); }
			protected set { _childcategories = value; }
		}

	}
}
