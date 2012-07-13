using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CMS.Web.FrameWork.Models;
using System.Web.Routing;

namespace CMS.Web.FrameWork.Helpers {
	public static class JQueryAjaxTableHelper {
		public static MvcHtmlString JQueryAjaxTable(this HtmlHelper helper, string id, JQueryAjaxTableOption option) {
			TagBuilder tag;
			StringBuilder table = new StringBuilder();
			TagBuilder tableTag = new TagBuilder("table");
			if (option.Attributes != null) {
				tableTag.MergeAttributes(new RouteValueDictionary(option.Attributes));
			}
			tableTag.MergeAttributes(new RouteValueDictionary(new Dictionary<string, object>() {
				 { "id", id }
			}));
			StringBuilder dataTemplate = new StringBuilder();
			table.Append(tableTag.ToString(TagRenderMode.StartTag));
			table.Append("<thead>");
			foreach (var column in option.Columns) {
				if (String.IsNullOrEmpty(column.HeaderTemplate)) {
					column.HeaderTemplate = column.DisplayName;
				}
				if (String.IsNullOrEmpty(column.ColumTemplate)) {
					column.ColumTemplate = "${" + column.DataFieldName + "}";
				}
				StringBuilder headerAttributes = new StringBuilder();
				StringBuilder columnAttributes = new StringBuilder();
				IDictionary<string, object> styles = new Dictionary<string, object>();
				switch (column.Align) {
					case Align.Center:
						styles.Add("style", "text-align:center");
						break;
					case Align.Left:
						styles.Add("style", "text-align:left");
						break;
					case Align.Right:
						styles.Add("style", "text-align:right");
						break;
				}

				tag = new TagBuilder("th");
				if (column.HeaderAttributes != null) {
					tag.MergeAttributes(new RouteValueDictionary(column.HeaderAttributes));
				}
				tag.MergeAttributes(new RouteValueDictionary(styles));
				tag.Attributes.Add("sortname", column.SortName);
				tag.AddCssClass(column.ClassName);
				tag.InnerHtml = column.HeaderTemplate;
				table.Append(tag.ToString());

				tag = new TagBuilder("td");
				if (column.ColumnAttributes != null) {
					tag.MergeAttributes(new RouteValueDictionary(column.ColumnAttributes));
				}
				tag.MergeAttributes(new RouteValueDictionary(styles));
				tag.AddCssClass(column.ClassName);
				tag.InnerHtml = column.ColumTemplate;
				dataTemplate.Append(tag.ToString());
			}
			table.Append("</thead>");
			table.Append(tableTag.ToString(TagRenderMode.EndTag));
			table.AppendFormat("<script id=\"bootstrapTableTemplate_{0}\" type=\"text/x-jquery-tmpl\">", id);
			table.Append("{{each(i,row) rows}}");
			table.Append("<tr>");
			table.Append(dataTemplate.ToString());
			table.Append("</tr>");
			table.Append("{{/each}}");
			table.Append("</script>");
			table.Append("<script>");
			table.Append("$(document).ready(function(){");
			table.Append("$(\"#" + id + "\").bootstrapTable({");
			table.AppendFormat("url:\"{0}\"", option.Url);
			StringBuilder onsuccessCallBack = new StringBuilder();
			onsuccessCallBack.Append("function(t,data){$(\"tbody\",t).remove();$(\"#bootstrapTableTemplate_" + id + "\").tmpl(data).appendTo(t);");
			onsuccessCallBack.Append("}");
			table.AppendFormat(",onSuccess:{0}", onsuccessCallBack.ToString());
			if (string.IsNullOrEmpty(option.OnSubmit) == false) {
				table.AppendFormat(",onSubmit:{0}", option.OnSubmit);
			}
			if (string.IsNullOrEmpty(option.OnError) == false) {
				table.AppendFormat(",onError:{0}", option.OnError);
			}
			if (option.IsPaging) {
				table.Append(",paging:true");
				table.AppendFormat(",pagesize:{0}", option.pagesize);
				table.AppendFormat(",pageindex:{0}", option.pageindex);
			}
			table.AppendFormat(",sortname:\"{0}\"", option.sortname);
			table.AppendFormat(",sortorder:\"{0}\"", option.sortorder);
			if (option.Parameters != null) {
				int index = 0;
				if (option.Parameters.Count() > 0) {
					table.Append(",params:[");
				}
				index = 0;
				foreach (var parameter in option.Parameters) {
					if (index > 0) {
						table.Append(",");
					}
					table.Append("{").AppendFormat("name:\"{0}\",value:\"{1}\"", parameter.Name, parameter.Value).Append("}");
					index++;
				}
				table.Append("]");
			}
			table.Append("});");
			table.Append("});");
			table.Append("</script>");
			return MvcHtmlString.Create(table.ToString());
		}
	}

	public class JQueryAjaxTableOption : PagingModel {

		public JQueryAjaxTableOption() {
			this.Url = string.Empty;
		}

		public List<JQueryAjaxTableColumn> Columns { get; set; }

		public string Url { get; set; }

		public string OnSubmit { get; set; }

		public string OnError { get; set; }

		public bool IsPaging { get; set; }

		public IDictionary<string, object> Attributes { get; set; }

		public List<JQueryAjaxTableParameter> Parameters { get; set; }
	}

	public class JQueryAjaxTableParameter {

		public string Name { get; set; }

		public object Value { get; set; }
	}

	public class JQueryAjaxTableColumn {

		public JQueryAjaxTableColumn() {
			this.DisplayName = string.Empty;
			this.ColumTemplate = string.Empty;
			this.HeaderTemplate = string.Empty;
			this.DataFieldName = string.Empty;
			this.ClassName = string.Empty;
		}

		public string DisplayName { get; set; }

		public string ClassName { get; set; }

		public string DataFieldName { get; set; }

		public string HeaderTemplate { get; set; }

		public string ColumTemplate { get; set; }

		public string SortName { get; set; }

		public Align Align { get; set; }

		public IDictionary<string, object> HeaderAttributes { get; set; }

		public IDictionary<string, object> ColumnAttributes { get; set; }
	}


}
