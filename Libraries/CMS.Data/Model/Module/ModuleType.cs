using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data {
	public partial class ModuleType : BaseEntity {

		public virtual int ModuleTypeID { get; set; }

		[Required(ErrorMessage = "ModuleTypeName is required")]
		[StringLength(20, ErrorMessage = "ModuleTypeName must be under 50 characters")]
		public virtual string ModuleTypeName { get; set; }

		private ICollection<Module> _modules;
		public virtual ICollection<Module> Modules {
			get { return _modules ?? (_modules = new List<Module>()); }
			protected set { _modules = value; }
		}
	}
}
