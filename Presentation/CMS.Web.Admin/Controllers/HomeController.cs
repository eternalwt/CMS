using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Linq;
using CMS.Web.FrameWork.Helpers;
using CMS.Web.FrameWork.Models;

namespace CMS.Web.Admin.Controllers {

	public class HomeController : Controller {

		//
		// GET: /Default1/

		public ActionResult Index() {
			return View();
		}


		[ChildActionOnly]
		public ActionResult Menus() {
			string fileName = System.IO.Path.Combine(Server.MapPath("~/App_Data"), "Admin.sitemap");
			FileInfo fileInfo = new System.IO.FileInfo(fileName);
			XDocument xmlDoc = XDocument.Load(fileName);
			List<MenuModel> menus = MenuHelper.GetMenus(xmlDoc);
			return View(menus);
		}

	}
}
