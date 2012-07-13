using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	/// <summary>
	/// Base class for entities
	/// </summary>
	public abstract partial class BaseEntity {

		[Required(ErrorMessage = "Created date is required")]
		public virtual DateTime CreatedDate { get; set; }

		[Required(ErrorMessage = "CreatedBy is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "CreatedBy is required")]
		public virtual int? CreatedBy { get; set; }

		[Required(ErrorMessage = "Last updated date is required")]
		public virtual DateTime LastUpdatedDate { get; set; }

		[Required(ErrorMessage = "LastUpdatedBy is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "LastUpdatedBy is required")]
		public virtual int? LastUpdatedBy { get; set; }
		 
	}
}
