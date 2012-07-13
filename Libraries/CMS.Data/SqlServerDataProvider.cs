using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;

namespace CMS.Data {
	public class SqlServerDataProvider : BaseEfDataProvider {
		public override IDbConnectionFactory GetConnectionFactory() {
			return new SqlConnectionFactory();
		}
	}
}
