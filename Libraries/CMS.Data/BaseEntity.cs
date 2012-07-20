using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;
using CMS.Data.Helpers;

namespace CMS.Data {
	/// <summary>
	/// Base class for entities
	/// </summary>
	public abstract partial class BaseEntity {

        [DateRange(ErrorMessage = "Invalid CreatedDate")]
		public virtual DateTime? CreatedDate { get; set; }

		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "CreatedBy is required")]
		public virtual int? CreatedBy { get; set; }

        [DateRange(ErrorMessage = "Invalid UpdatedDate")]
		public virtual DateTime? LastUpdatedDate { get; set; }

		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "LastUpdatedBy is required")]
		public virtual int? LastUpdatedBy { get; set; }
		 
	}
}
