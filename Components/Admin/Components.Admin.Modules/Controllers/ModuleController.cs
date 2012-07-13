using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Components.Admin.Modules.Controllers {
	/// <summary>
	/// Manages actions of the blog.
	/// </summary>
	[Export(typeof(IController)), ExportMetadata("Name", "Module")]
	public class ModuleController : Controller {
		#region Actions
		/// <summary>
		/// Displays the blog index.
		/// </summary>
		public ActionResult Index() {
			return View();
		}
		#endregion
	}
}
