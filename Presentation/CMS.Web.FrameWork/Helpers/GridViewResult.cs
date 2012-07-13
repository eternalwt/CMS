using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.FrameWork.Helpers {
	public class GridViewResult<T> {

		public int total { get; set; }

		public int totalpages { get; set; }

		public IEnumerable<T> rows { get; set; }
	}
}
