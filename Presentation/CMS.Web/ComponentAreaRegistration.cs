using System.Web.Mvc;

namespace CMS.Web {
	public class ComponentAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Component";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
			   "Component_default",
			   "Component/{controller}/{action}/{id}",
			    new { controller = "Home", action = "Index", area = "Component", id = "" },
				new[] { "CMS.Web.Components.Controllers" } 
			);
		}
	}
}
               