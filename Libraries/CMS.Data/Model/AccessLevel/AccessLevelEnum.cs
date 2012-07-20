using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class AccessLevel : BaseEntity {

		public virtual int AccessLevelID { get; set; }

		[Required(ErrorMessage = "AccessLevel name is required")]
		[StringLength(20, ErrorMessage = "AccessLevel name must be under 20 characters")]
		public virtual string AccessLevelName { get; set; }

		private ICollection<Menu> _menus;
		public virtual ICollection<Menu> Menus {
			get { return _menus ?? (_menus = new List<Menu>()); }
			protected set { _menus = value; }
		}

		private ICollection<Module> _modules;
		public virtual ICollection<Module> Modules {
			get { return _modules ?? (_modules = new List<Module>()); }
			protected set { _modules = value; }
		}
	}
}
