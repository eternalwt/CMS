using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Web.FrameWork.Helpers {
	public class MvcForm1 : IDisposable {

		private bool _disposed;
		private HttpResponseBase _httpResponse;

		public delegate void DisposeEventHandler(HttpResponseBase httpResponse);
		public event DisposeEventHandler OnDispose;

		public MvcForm1(HttpResponseBase httpResponse) {
			_httpResponse = httpResponse;
		}

		[SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
		public void Dispose() {
			Dispose(true /* disposing */);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing) {
			if (!_disposed) {
				_disposed = true;
				if (this.OnDispose != null) {
					this.OnDispose(_httpResponse);
				}
				_httpResponse.Write("</form>");
			}
		}

		public void EndMenu() {
			Dispose(true);
		}
	}
}
