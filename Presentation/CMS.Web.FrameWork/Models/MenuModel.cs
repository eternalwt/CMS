using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.FrameWork.Models {
	public class MenuModel {

		public MenuModel() {
			Attributes = new Dictionary<string, object>();
			Childs = new List<MenuModel>();
			this.Title = string.Empty;
			this.Url = string.Empty;
			this.Parent = null;
			this.ID = 0;
		}

		public int ID { get; set; }

		public MenuModel Parent { get; set; }

		public string Title { get; set; }

		public string Url { get; set; }

		public IDictionary<string, object> Attributes { get; set; }

		public List<MenuModel> Childs { get; set; }

	}
}
