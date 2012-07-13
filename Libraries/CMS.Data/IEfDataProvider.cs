using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;

namespace CMS.Data {

	public interface IEfDataProvider : IDataProvider {
		IDbConnectionFactory GetConnectionFactory();

		void InitConnectionFactory();

		void SetDatabaseInitializer();
	}
}
