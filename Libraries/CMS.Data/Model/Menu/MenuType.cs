using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class MenuType : BaseEntity {

		public virtual int MenuTypeID { get; set; }

		[Required(ErrorMessage = "MenuTypeName is required")]
		[StringLength(50, ErrorMessage = "MenuTypeName must be under 50 characters")]
		public virtual string MenuTypeName { get; set; }
 
		public virtual string Description { get; set; }

		private ICollection<Menu> _menus;
		public virtual ICollection<Menu> Menus {
			get { return _menus ?? (_menus = new List<Menu>()); }
			protected set { _menus = value; }
		}
	}
}
