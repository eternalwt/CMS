using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Admin.Models {
	public class AutoCompleteListModel {

	public AutoCompleteListModel() {
			id = 0;
			label = string.Empty;
			value = string.Empty;
		}
		public int id { get; set; }
		public string label { get; set; }
		public string value { get; set; }
	}
}