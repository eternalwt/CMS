using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace CMS.Data {
	public abstract class BaseEfDataProvider : IEfDataProvider {
		public abstract IDbConnectionFactory GetConnectionFactory();

		public void InitConnectionFactory() {
			Database.DefaultConnectionFactory = GetConnectionFactory();
		}

		public virtual void SetDatabaseInitializer() {
			//pass some table names to ensure that we have nopCommerce 2.X installed
			var initializer = new CreateTablesIfNotExist<CMSContext>(new[] { "Module", "ModuleType" });
			Database.SetInitializer<CMSContext>(initializer);
		}

		public virtual void InitDatabase() {
			InitConnectionFactory();
			SetDatabaseInitializer();
		}
	} 
}
