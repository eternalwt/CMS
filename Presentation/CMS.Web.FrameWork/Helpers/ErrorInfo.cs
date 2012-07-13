using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.FrameWork.Helpers {

	public class ErrorInfo {
		public ErrorInfo(string propertyName, string errorMessage) {
			this.PropertyName = propertyName;
			this.ErrorMessage = errorMessage;
		}
		//public ErrorInfo(string propertyName, string errorMessage, object onObject) {
		//    this.PropertyName = propertyName;
		//    this.ErrorMessage = errorMessage;
		//    this.Object = onObject;
		//}

		public string ErrorMessage { get; set; }
		//public object Object { get; set; }
		public string PropertyName { get; set; }
	}
}
