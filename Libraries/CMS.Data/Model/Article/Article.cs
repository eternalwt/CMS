using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data.Config;

namespace CMS.Data {
	public partial class Article : BaseEntity {

		public virtual int ArticleID { get; set; }

		[Required(ErrorMessage = "Article name is required")]
		[StringLength(256, ErrorMessage = "Article name must be under 256 characters")]
		public virtual string ArticleName { get; set; }

		[Required(ErrorMessage = "Section is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Section is required")]
		public virtual int SectionID { get; set; }

		[Required(ErrorMessage = "Category is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Category is required")]
		public virtual int CategoryID { get; set; }

		[Required(ErrorMessage = "Introduction text is required")]
		public virtual string IntroductionText { get; set; }

		[Required(ErrorMessage = "Full text is required")]
		public virtual string FullText { get; set; }
	 
		[Required(ErrorMessage = "IsPublished is required")]
		public virtual bool IsPublished { get; set; }

		[Required(ErrorMessage = "Sort order is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Sort order is required")]
		public virtual int SortOrder { get; set; }

		public virtual string Parameters { get; set; }
		
		[Required(ErrorMessage = "Access level is required")]
		[Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Access level is required")]
		public virtual int AccessLevelID { get; set; }

		public virtual int? ParentID { get; set; }

		public virtual string MetaData { get; set; }

		public virtual string MetaDescription { get; set; }

		public virtual string MetaKeywords { get; set; }
		
		public virtual DateTime? PublishUp { get; set; }

		public virtual DateTime? PublishDown { get; set; }

		public virtual Section Section { get; set; }

		public virtual Category Category { get; set; }

		public virtual Article ParentArticle { get; set; }

		private ICollection<Article> _childArticles;
		public virtual ICollection<Article> ChildArticles {
			get { return _childArticles ?? (_childArticles = new List<Article>()); }
			protected set { _childArticles = value; }
		}


	}
}
