using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.FrameWork.Helpers {
	public class JsonResultInfo {

		public JsonResultInfo() {
			this.Errors = new List<ErrorInfo>();
		}

		public int ID { get; set; }

		public bool IsSuccess {
			get {
				return (this.Errors.Any() == false);
			}
		}

		public IEnumerable<ErrorInfo> Errors { get; set; }

	}
}
