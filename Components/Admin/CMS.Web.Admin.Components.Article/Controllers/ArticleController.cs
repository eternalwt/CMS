using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MefMvcFramework.Web;

namespace CMS.Web.Components.Controllers {

	[ExportController("Article")]
	public class ArticleController : Controller {

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
