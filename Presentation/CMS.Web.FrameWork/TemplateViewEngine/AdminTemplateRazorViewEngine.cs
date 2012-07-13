using System;
using System.Web.Mvc;

namespace CMS.Web.FrameWork.TemplateViewEngine {

	public class AdminTemplateRazorViewEngine : TemplateBuildManagerViewEngine {
		public AdminTemplateRazorViewEngine() {


			AreaViewLocationFormats = new[] {
			                                    "~/AdminTemplates/{3}/{2}/{1}/{0}.cshtml",
			                                    "~/AdminTemplates/{3}/{2}/{1}/{0}.vbhtml",
			                                    "~/AdminTemplates/{3}/{2}/Shared/{0}.cshtml",
			                                    "~/AdminTemplates/{3}/{2}/Shared/{0}.vbhtml",
 
												
												"~/Admin/{2}/{1}/{0}.cshtml",
			                                    "~/Admin/{2}/{1}/{0}.vbhtml",
			                                    "~/Admin/{2}/Shared/{0}.cshtml",
			                                    "~/Admin/{2}/Shared/{0}.vbhtml",
			                                };

			AreaMasterLocationFormats = new[] {
												  "~/AdminTemplates/{3}/{2}/{1}/{0}.cshtml",
			                                      "~/AdminTemplates/{3}/{2}/{1}/{0}.vbhtml",
			                                      "~/AdminTemplates/{3}/{2}/Shared/{0}.cshtml",
			                                      "~/AdminTemplates/{3}/{2}/Shared/{0}.vbhtml",
 
												  
												  "~/Admin/{2}/{1}/{0}.cshtml",
			                                      "~/Admin/{2}/{1}/{0}.vbhtml",
			                                      "~/Admin/{2}/Shared/{0}.cshtml",
			                                      "~/Admin/{2}/Shared/{0}.vbhtml",
			                                  };

			AreaPartialViewLocationFormats = new[] {
													   "~/AdminTemplates/{3}/{2}/{1}/{0}.cshtml",
			                                           "~/AdminTemplates/{3}/{2}/{1}/{0}.vbhtml",
			                                           "~/AdminTemplates/{3}/{2}/Shared/{0}.cshtml",
			                                           "~/AdminTemplates/{3}/{2}/Shared/{0}.vbhtml",

													   	"~/Admin/{2}/{1}/{0}.cshtml",
			                                           "~/Admin/{2}/{1}/{0}.vbhtml",
			                                           "~/Admin/{2}/Shared/{0}.cshtml",
			                                           "~/Admin/{2}/Shared/{0}.vbhtml",
			                                       };


			ViewLocationFormats = null;

			MasterLocationFormats = null;

			PartialViewLocationFormats = null;

			ViewStartFileExtensions = new[] { "cshtml", "vbhtml", };
		}

		public string[] ViewStartFileExtensions { get; set; }

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
			return new RazorView(controllerContext, partialPath, null, false, ViewStartFileExtensions);
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
			return new RazorView(controllerContext, viewPath, masterPath, true, ViewStartFileExtensions);
		}

		protected override bool IsValidCompiledType(ControllerContext controllerContext, string virtualPath, Type compiledType) {
			// return typeof(WebViewPage).IsAssignableFrom(compiledType);
			return false;
		}
	}
}