using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Data.Service.Helpers {
	/// <summary>
	/// Paged list interface
	/// </summary>
	public interface IPagedList<T> : IList<T> {
		int PageIndex { get; }
		int PageSize { get; }
		int TotalCount { get; }
		int TotalPages { get; }
	}
}
