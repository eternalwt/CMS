using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace CMS.Web.FrameWork.Helpers {
	public class JavaScript : IDisposable {

		private bool _disposed;
		private ViewContext _viewContext;

		public delegate void DisposeEventHandler(HttpResponseBase httpResponse);
		public event DisposeEventHandler OnDispose;

		public JavaScript(ViewContext viewContext) {
			_viewContext = viewContext;
		}

		[SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
		public void Dispose() {
			Dispose(true /* disposing */);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing) {
			if (!_disposed) {
				_disposed = true;
				_viewContext.Writer.Write("</script>");
			}
		}

		public void EndTemplateScript() {
			Dispose(true);
		}
	}
}
