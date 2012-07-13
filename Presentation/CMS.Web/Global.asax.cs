using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MefMvcFramework.Web;
using CMS.Web.FrameWork.TemplateViewEngine;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Hosting;
using CMS.Data;

namespace CMS.Web {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : Application {
 
		#region Methods
		/// <summary>
		/// Creates the instance of the Unity container.
		/// </summary>
		protected override void PreCompose() {
			Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", HostingEnvironment.MapPath("~/App_Data/"), "");
			var initializer = new InstallDefaultData<CMSContext>();
			Database.SetInitializer<CMSContext>(initializer);

			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
			RegisterViewEngine(ViewEngines.Engines);
		}

	 
		#endregion

		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("favicon.ico");
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new[] { "CMS.Web.Public.Controllers" }  // Parameter defaults
			);
		}

		 

		public static void RegisterViewEngine(ViewEngineCollection viewEngines) {
			//// We do not need the default view engine
			viewEngines.Clear();

			//var adminTemplateRazorViewEngine = new AdminTemplateRazorViewEngine {
			//    CurrentTemplate = httpContext => httpContext.Session["theme"] as string
			//};

			//viewEngines.Add(adminTemplateRazorViewEngine);

			var templateRazorViewEngine = new TemplateRazorViewEngine {
				CurrentTemplate = httpContext => httpContext.Session["theme"] as string
			};

			viewEngines.Add(templateRazorViewEngine);

		
		}
	}
}