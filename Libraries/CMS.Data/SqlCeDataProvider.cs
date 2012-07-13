using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Web.Hosting;
using System.Data.Entity;

namespace CMS.Data {
	public class SqlCeDataProvider : BaseEfDataProvider {
		public override IDbConnectionFactory GetConnectionFactory() {
			return new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", HostingEnvironment.MapPath("~/App_Data/"), "");
		}
		public override void SetDatabaseInitializer() {
			var initializer = new CreateDatabaseIfNotExists<CMSContext>();
			Database.SetInitializer<CMSContext>(initializer);
		}
	}
}
