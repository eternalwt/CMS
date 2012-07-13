using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CMS.Web.FrameWork.Models;

namespace CMS.Web.FrameWork.Helpers {
	public class MenuHelper {

		public static List<MenuModel> GetMenus(XDocument document) {
			List<MenuModel> menus = new List<MenuModel>();
			List<XElement> siteMapNodes = (from map in document.Elements("siteMap").Elements("siteMapNode")
										   select map).ToList();
			MenuModel menu = null;
			foreach (var siteMap in siteMapNodes) {
				menu = GetMenu(siteMap);
				LoadMenus(siteMap, ref menu);
				menus.Add(menu);
			}
			return menus;
		}


		private static void LoadMenus(XElement siteMapNode, ref MenuModel menu) {
			List<XElement> siteMapNodes = (from map in siteMapNode.Elements()
										   select map).ToList();
			MenuModel submenu = null;
			if (siteMapNodes.Count() > 0) {
				foreach (var siteMap in siteMapNodes) {
					submenu = GetMenu(siteMap);
					submenu.Parent = menu;
					menu.Childs.Add(submenu);
					LoadMenus(siteMap, ref submenu);
				}
			}
		}

		private static MenuModel GetMenu(XElement siteMapNode) {
			string title = string.Empty;
			MenuModel menu = null;
			menu = new MenuModel();
			var arributes = siteMapNode.Attributes().ToList();
			foreach (var a in arributes) {
				switch (a.Name.ToString()) {
					case "url":
						menu.Url = a.Value; break;
					case "title":
						menu.Title = a.Value; break;
					default:
						menu.Attributes.Add(a.Name.ToString(), a.Value.ToString());
						break;
				}
			}
			return menu;
		}
	}
}
