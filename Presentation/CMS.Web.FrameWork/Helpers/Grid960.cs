using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CMS.Web.FrameWork.Models;

namespace CMS.Web.FrameWork.Helpers {
	public static class Grid960Helper {

		public static MvcHtmlString Grid960(this HtmlHelper helper, Grid960Option option) {
			TagBuilder tag = new TagBuilder("a");
			StringBuilder grid960 = new StringBuilder();
			StringBuilder dataTemplate = new StringBuilder();
			grid960.AppendFormat("<div id=\"{0}\">", option.ID, option.Grid960Length);
			grid960.AppendFormat("<div class=\"grid-header grid_{0}\">", option.Grid960Length);
			string className = string.Empty;
			string template = string.Empty;
			string headerTemplate = string.Empty;
			int index = 0;
			foreach (var gridCol in option.Columns) {
				if (gridCol.IsVisible == true || gridCol.Grid960Length <= 0)
					continue;
				template = string.Empty;
				if (string.IsNullOrEmpty(gridCol.Template) == false)
					template = gridCol.Template;
				else if (string.IsNullOrEmpty(gridCol.DataFieldName) == false)
					template = "${row." + gridCol.DataFieldName + "}";

				if (string.IsNullOrEmpty(template))
					continue;
				if (string.IsNullOrEmpty(gridCol.HeaderTemplate) == false)
					headerTemplate = gridCol.HeaderTemplate;
				else
					headerTemplate = gridCol.DisplayName;
				className = string.Empty;

				switch (gridCol.Align) {
					case Align.Left: className = "left"; break;
					case Align.Right: className = "right"; break;
					case Align.Center: className = "center"; break;
				}
				if (index == 0)
					className += " omega";
				if (index == option.Columns.Count - 1)
					className += " alpha";
				grid960.AppendFormat("<div class=\"grid_{0} {1} grid-cell\" sortname=\"{2}\">{3}</div>", gridCol.Grid960Length, className, gridCol.SortName, headerTemplate);
				dataTemplate.AppendFormat("<div class=\"grid_{0} {1} grid-cell\">{2}</div>", gridCol.Grid960Length, className, template);
				index++;
			}
			grid960.AppendFormat("<div class=\"clear\">&nbsp;</div></div><div class=\"grid-content grid_{0}\"></div><div class='clear'>&nbsp;</div></div>", option.Grid960Length);

			grid960.AppendFormat("<script id=\"960GridDataTemplate_{0}\" type=\"text/x-jquery-tmpl\">", option.ID);
			grid960.Append("{{each(i,row) rows}}");
			grid960.Append("<div class=\"{{if i%2>0}}arow{{else}}row{{/if}}\">");
			grid960.Append(dataTemplate.ToString());
			grid960.Append("</div><div class=\"clear\">&nbsp;</div>");
			grid960.Append("{{/each}}");
			grid960.Append("</script>");
			StringBuilder onsuccessCallBack = new StringBuilder();
			onsuccessCallBack.Append("function(grid,data){var $gridcontent=$(\"<div class='grid-content grid_" + option.Grid960Length + "'></div>\");");
			onsuccessCallBack.Append("$gridcontent.empty();$(\"#960GridDataTemplate_" + option.ID + "\").tmpl(data).appendTo($gridcontent);");
			onsuccessCallBack.Append("$(\".grid-content\",grid).remove();$(\".grid-header\",grid).after($gridcontent);");
			if (string.IsNullOrEmpty(option.OnSuccess)==false) {
				onsuccessCallBack.Append("if(").AppendFormat("{0}",option.OnSuccess).Append("){").AppendFormat("{0}(grid,data);", option.OnSuccess).Append("}");
			}
			onsuccessCallBack.Append("}");
			option.OnSuccess = onsuccessCallBack.ToString();

			grid960.Append("<script>");
			grid960.Append("$(document).ready(function(){");
			grid960.Append("$(\"#" + option.ID + "\").grid960({");
			grid960.AppendFormat("gridLength:{0}", option.Grid960Length);
			if (string.IsNullOrEmpty(option.Url) == false) {
				grid960.AppendFormat(",url:\"{0}\"", option.Url);
			}
			if (string.IsNullOrEmpty(option.OnSuccess) == false) {
				grid960.AppendFormat(",onSuccess:{0}", option.OnSuccess);
			}
			if (string.IsNullOrEmpty(option.OnSubmit) == false) {
				grid960.AppendFormat(",onSubmit:{0}", option.OnSubmit);
			}
			if (string.IsNullOrEmpty(option.OnError) == false) {
				grid960.AppendFormat(",onError:{0}", option.OnError);
			}
			if (option.IsPaging) {
				grid960.Append(",paging:true");
				grid960.AppendFormat(",pagesize:{0}", option.pagesize);
				grid960.AppendFormat(",pageindex:{0}", option.pageindex);
			}
			grid960.AppendFormat(",sortname:\"{0}\"", option.sortname);
			grid960.AppendFormat(",sortorder:\"{0}\"", option.sortorder);
			if (option.Parameters != null) {
				if (option.Parameters.Count() > 0) {
					grid960.Append(",params:[");
				}
				index = 0;
				foreach (var parameter in option.Parameters) {
					if (index > 0) {
						grid960.Append(",");
					}
					grid960.Append("{").AppendFormat("name:\"{0}\",value:\"{1}\"", parameter.Name, parameter.Value).Append("}");
					index++;
				}
				grid960.Append("]");
			}
			grid960.Append("});");
			grid960.Append("});");
			grid960.Append("</script>");
			return MvcHtmlString.Create(grid960.ToString());
		}
	}

	public class Grid960Option : PagingModel {

		public string ID { get; set; }

		public int Grid960Length { get; set; }

		public List<Grid960Column> Columns { get; set; }

		public string Url { get; set; }

		public string OnSuccess { get; set; }

		public string OnSubmit { get; set; }

		public string OnError { get; set; }

		public bool IsPaging { get; set; }

		public List<Grid960Parameter> Parameters { get; set; }
	}

	public class Grid960Parameter {

		public string Name { get; set; }

		public object Value { get; set; }
	}

	public class Grid960Column {

		public Grid960Column() {
			this.DisplayName = string.Empty;
			this.DataFieldName = string.Empty;
			this.Template = string.Empty;
			this.IsVisible = false;
			this.Grid960Length = 0;
		}

		public string DisplayName { get; set; }

		public string DataFieldName { get; set; }

		public string HeaderTemplate { get; set; }

		public string Template { get; set; }

		public bool IsVisible { get; set; }

		public int Grid960Length { get; set; }

		public string SortName { get; set; }

		public Align Align { get; set; }
	}

	public enum Align {

		Left = 0,
		Center = 1,
		Right = 2
	}
}
