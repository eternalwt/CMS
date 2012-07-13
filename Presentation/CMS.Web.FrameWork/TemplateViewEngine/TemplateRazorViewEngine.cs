using System;
using System.Web.Mvc;

namespace CMS.Web.FrameWork.TemplateViewEngine {

	public class TemplateRazorViewEngine : TemplateBuildManagerViewEngine {
		public TemplateRazorViewEngine() {

			AreaViewLocationFormats = new[] {
			                                    "~/Templates/{3}/{2}/{1}/{0}.cshtml",
			                                    "~/Templates/{3}/{2}/{1}/{0}.vbhtml",
			                                    "~/Templates/{3}/{2}/Shared/{0}.cshtml",
			                                    "~/Templates/{3}/{2}/Shared/{0}.vbhtml",

												"~/Views/{2}/{1}/{0}.cshtml",
			                                    "~/Views/{2}/{1}/{0}.vbhtml",
			                                    "~/Views/{2}/Shared/{0}.cshtml",
			                                    "~/Views/{2}/Shared/{0}.vbhtml",
			                                };

			AreaMasterLocationFormats = new[] {
												  "~/Templates/{3}/{2}/{1}/{0}.cshtml",
			                                      "~/Templates/{3}/{2}/{1}/{0}.vbhtml",
			                                      "~/Templates/{3}/{2}/Shared/{0}.cshtml",
			                                      "~/Templates/{3}/{2}/Shared/{0}.vbhtml",

												  "~/Views/{2}/{1}/{0}.cshtml",
			                                      "~/Views/{2}/{1}/{0}.vbhtml",
			                                      "~/Views/{2}/Shared/{0}.cshtml",
			                                      "~/Views/{2}/Shared/{0}.vbhtml",
			                                  };

			AreaPartialViewLocationFormats = new[] {
													   "~/Templates/{3}/{2}/{1}/{0}.cshtml",
			                                           "~/Templates/{3}/{2}/{1}/{0}.vbhtml",
			                                           "~/Templates/{3}/{2}/Shared/{0}.cshtml",
			                                           "~/Templates/{3}/{2}/Shared/{0}.vbhtml",

														"~/Views/{2}/{1}/{0}.cshtml",
			                                           "~/Views/{2}/{1}/{0}.vbhtml",
			                                           "~/Views/{2}/Shared/{0}.cshtml",
			                                           "~/Views/{2}/Shared/{0}.vbhtml",
			                                       };

			ViewLocationFormats = new[] {
                                            "~/Templates/{2}/{1}/{0}.cshtml",
                                            "~/Templates/{2}/{1}/{0}.vbhtml",
                                            "~/Templates/{2}/Shared/{0}.cshtml",
                                            "~/Templates/{2}/Shared/{0}.vbhtml",

										    "~/Views/{1}/{0}.cshtml",
                                            "~/Views/{1}/{0}.vbhtml",
                                            "~/Views/Shared/{0}.cshtml",
                                            "~/Views/Shared/{0}.vbhtml",
                                        };

			MasterLocationFormats = new[] {
                                              "~/Templates/{2}/{1}/{0}.cshtml",
                                              "~/Templates/{2}/{1}/{0}.vbhtml",
                                              "~/Templates/{2}/Shared/{0}.cshtml",
                                              "~/Templates/{2}/Shared/{0}.vbhtml",

											  "~/Views/{1}/{0}.cshtml",
                                              "~/Views/{1}/{0}.vbhtml",
                                              "~/Views/Shared/{0}.cshtml",
                                              "~/Views/Shared/{0}.vbhtml",
                                          };

			PartialViewLocationFormats = new[] {
                                                   "~/Templates/{2}/{1}/{0}.cshtml",
                                                   "~/Templates/{2}/{1}/{0}.vbhtml",
                                                   "~/Templates/{2}/Shared/{0}.cshtml",
                                                   "~/Templates/{2}/Shared/{0}.vbhtml",

												   "~/Views/{1}/{0}.cshtml",
                                                   "~/Views/{1}/{0}.vbhtml",
                                                   "~/Views/Shared/{0}.cshtml",
                                                   "~/Views/Shared/{0}.vbhtml",
                                               };

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