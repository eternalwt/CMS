﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Data.Service.Helpers {
	/// <summary>
	/// Paged list
	/// </summary>
	/// <typeparam name="T">T</typeparam>
	public class PagedList<T> : List<T>, IPagedList<T> {
		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		public PagedList(IQueryable<T> source, int pageIndex, int pageSize) {
			int total = source.Count();
			this.TotalCount = total;
			this.TotalPages = total / pageSize;

			if (total % pageSize > 0)
				TotalPages++;

			this.PageSize = pageSize;
			this.PageIndex = pageIndex;
			this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		public PagedList(IList<T> source, int pageIndex, int pageSize) {
			TotalCount = source.Count();
			TotalPages = TotalCount / pageSize;

			if (TotalCount % pageSize > 0)
				TotalPages++;

			this.PageSize = pageSize;
			this.PageIndex = pageIndex;
			this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
		}

		public int PageIndex { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }
		public int TotalPages { get; private set; }

	 
	}
}
