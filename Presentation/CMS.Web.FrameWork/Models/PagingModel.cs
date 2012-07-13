using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.FrameWork.Models {
	public class PagingModel {

		public PagingModel() {
			this.pageindex = 1;
			this.pagesize = 20;
			this.sortname = string.Empty;
			this.sortorder = "asc";
		}

		public int pageindex { get; set; }

		public int pagesize { get; set; }

		public string sortname { get; set; }

		public string sortorder { get; set; }
	}

	 
}
