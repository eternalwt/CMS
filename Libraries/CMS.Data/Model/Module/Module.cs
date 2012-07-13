using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;
using CMS.Data.Helpers;

namespace CMS.Data {
	public partial class Module : BaseEntity {

		public virtual int ModuleID { get; set; }

		[Required(ErrorMessage = "Module name is required")]
		[StringLength(50, ErrorMessage = "Module name must be under 50 characters")]
		public virtual string ModuleName { get; set; }

		[Required(ErrorMessage = "Module type is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Module type is required")]
		public virtual int ModuleTypeID { get; set; }

		[Required(ErrorMessage = "Position is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Position is required")]
		public virtual int PositionID { get; set; }

		[Required(ErrorMessage = "IsPublish is required")]
		public virtual bool IsPublish { get; set; }

		[DateRange(ErrorMessage = "Invalid PublishUp Date")]
		public virtual DateTime? PublishUp { get; set; }

		[DateRange(ErrorMessage = "Invalid PublishDown Date")]
		public virtual DateTime? PublishDown { get; set; }

		[Required(ErrorMessage = "Sort order is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Sort order is required")]
		public virtual int SortOrder { get; set; }

		public virtual string Parameters { get; set; }

		[Required(ErrorMessage = "Access level is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Access level is required")]
		public virtual int AccessLevelID { get; set; }

		public virtual AccessLevel AccessLevel { get; set; }

		public virtual ModuleType ModuleType { get; set; }

		private ICollection<ModulesInRoles> _modulesinroles;
		public virtual ICollection<ModulesInRoles> ModulesInRoles {
			get { return _modulesinroles ?? (_modulesinroles = new List<ModulesInRoles>()); }
			protected set { _modulesinroles = value; }
		}

		private ICollection<ModulesInMenus> _modulesinmenus;
		public virtual ICollection<ModulesInMenus> ModulesInMenus {
			get { return _modulesinmenus ?? (_modulesinmenus = new List<ModulesInMenus>()); }
			protected set { _modulesinmenus = value; }
		}
		
		public virtual Position Position { get; set; }

	}
}
