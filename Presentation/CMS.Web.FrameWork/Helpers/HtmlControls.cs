using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Web.Mvc.Html;

namespace CMS.Web.FrameWork.Helpers {
	public static class HtmlControls {

		#region Url
		public static string Url(this HtmlHelper helper) {
			return new UrlHelper(helper.ViewContext.RequestContext).Content("~/");
		}
		public static string Url(this HtmlHelper helper, string content) {
			if (content != null) {
				if (content.StartsWith("~/") == false) {
					content = "~" + (content.StartsWith("/") == false ? "/" + content : content);
				}
			}
			return new UrlHelper(helper.ViewContext.RequestContext).Content(content);
		}
		public static string Url(this HtmlHelper helper, string actionName, string controllerName) {
			return new UrlHelper(helper.ViewContext.RequestContext).Action(actionName, controllerName);
		}
		#endregion

		public static MvcForm Form(this HtmlHelper htmlHelper, object htmlAttributes) {
			return Form(htmlHelper, new RouteValueDictionary(htmlAttributes));
		}
		public static MvcForm Form(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes) {
			TagBuilder tagBuilder = new TagBuilder("form");
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			return new MvcForm(htmlHelper.ViewContext);
		}


		#region Button
		public static MvcHtmlString Button(this HtmlHelper helper, string value, IDictionary<string, object> htmlAttributes) {
			TagBuilder tag = new TagBuilder("button");
			tag.MergeAttributes(htmlAttributes);
			return MvcHtmlString.Create(string.Format("{0}{1}{2}", tag.ToString(TagRenderMode.StartTag), value, "</button>"));
		}
		public static MvcHtmlString Button(this HtmlHelper helper, string value, object htmlAttributes) {
			TagBuilder tag = new TagBuilder("button");
			tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			return MvcHtmlString.Create(string.Format("{0}{1}{2}", tag.ToString(TagRenderMode.StartTag), value, "</button>"));
		}
		#endregion

		#region Anchor
		public static MvcHtmlString Anchor(this HtmlHelper helper, string innerHTML, string href) {
			return Anchor(helper, innerHTML, href, new { });
		}
		public static MvcHtmlString Anchor(this HtmlHelper helper, string innerHTML, string href, object htmlAttributes) {
			TagBuilder tag = new TagBuilder("a");
			if (string.IsNullOrEmpty(href) == false) {
				if (href.StartsWith("javascript") == false && href != "#") {
					href = Url(helper, href);
				}
				tag.Attributes.Add("href", href);
			}
			tag.InnerHtml = innerHTML;
			tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
		}
		public static MvcHtmlString Anchor(this HtmlHelper helper, string innerHTML, string href, IDictionary<string, object> htmlAttributes) {
			TagBuilder tag = new TagBuilder("a");
			if (string.IsNullOrEmpty(href) == false) {
				if (href.StartsWith("javascript") == false && href != "#") {
					href = Url(helper, href);
				}
				tag.Attributes.Add("href", href);
			}
			tag.InnerHtml = innerHTML;
			tag.MergeAttributes(htmlAttributes);
			return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
		}
		public static MvcHtmlString Anchor(this HtmlHelper helper, string innerHTML, object htmlAttributes) {
			return Anchor(helper, innerHTML, "#", htmlAttributes);
		}
		public static MvcHtmlString Anchor(this HtmlHelper helper, string innerHTML) {
			return Anchor(helper, innerHTML, "#", new { });
		}
		#endregion

		#region Javascript
		public static JavaScript JavaScript(this HtmlHelper helper) {
			TagBuilder tagBuilder = new TagBuilder("script");
			helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			return new JavaScript(helper.ViewContext);
		}
		#endregion

		#region jQueryTemplateScript
		public static JavaScript jQueryTemplateScript(this HtmlHelper helper, string id) {
			TagBuilder tagBuilder = new TagBuilder("script");
			tagBuilder.Attributes.Add("id", id);
			tagBuilder.Attributes.Add("type", "text/x-jquery-tmpl");
			helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			return new JavaScript(helper.ViewContext);
		}
	 
		#endregion
	}
}
