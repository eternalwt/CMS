using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data {
	public partial class Position : BaseEntity {

		public virtual int PositionID { get; set; }

		[Required(ErrorMessage = "Position name is required")]
		[StringLength(20, ErrorMessage = "Position name must be under 20 characters")]
		public virtual string PositionName { get; set; }

		private ICollection<Module> _modules;
		public virtual ICollection<Module> Modules {
			get { return _modules ?? (_modules = new List<Module>()); }
			protected set { _modules = value; }
		}

	}
}
