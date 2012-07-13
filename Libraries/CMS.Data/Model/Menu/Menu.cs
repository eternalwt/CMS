using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class Menu : BaseEntity {

		public virtual int MenuID { get; set; }

		[Required(ErrorMessage = "Menu name is required")]
		[StringLength(50, ErrorMessage = "Menu name must be under 50 characters")]
		public virtual string MenuName { get; set; }

		[Required(ErrorMessage = "Menu display name is required")]
		[StringLength(50, ErrorMessage = "Menu display name must be under 50 characters")]
		public virtual string DisplayName { get; set; }

		[Required(ErrorMessage = "Menu type is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Menu type is required")]
		public virtual int MenuTypeID { get; set; }

		[Required(ErrorMessage = "IsPublished is required")]
		public virtual bool IsPublished { get; set; }

		[Required(ErrorMessage = "Sort order is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Sort order is required")]
		public virtual int SortOrder { get; set; }

		public virtual string Parameters { get; set; }

		[Required(ErrorMessage = "IsHome is required")]
		public virtual bool IsHome { get; set; }

		[Required(ErrorMessage = "Access level is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Access level is required")]
		public virtual int AccessLevelID { get; set; }

		public virtual AccessLevel AccessLevel { get; set; }

		public virtual int? ParentID { get; set; }

		public virtual Menu ParentMenu { get; set; }

		private ICollection<Menu> _childmenus;
		public virtual ICollection<Menu> ChildMenus {
			get { return _childmenus ?? (_childmenus = new List<Menu>()); }
			protected set { _childmenus = value; }
		}

		public virtual DateTime? PublishUp { get; set; }

		public virtual DateTime? PublishDown { get; set; }

		public virtual MenuType MenuType { get; set; }

		private ICollection<MenusInRoles> _modulesinroles;
		public virtual ICollection<MenusInRoles> MenusInRoles {
			get { return _modulesinroles ?? (_modulesinroles = new List<MenusInRoles>()); }
			protected set { _modulesinroles = value; }
		}

		private ICollection<ModulesInMenus> _modulesinmenus;
		public virtual ICollection<ModulesInMenus> ModulesInMenus {
			get { return _modulesinmenus ?? (_modulesinmenus = new List<ModulesInMenus>()); }
			protected set { _modulesinmenus = value; }
		}

	}
}
