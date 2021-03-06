﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Transactions;
using System.Data.Entity.Infrastructure;

namespace CMS.Data {
	public class CreateTablesIfNotExist<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext {
		private string[] _tablesToValidate;
		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="tablesToValidate">A list of existing table names to validate; null to don't validate table names</param>
		public CreateTablesIfNotExist(string[] tablesToValidate)
			: base() {
			this._tablesToValidate = tablesToValidate;
		}


		public void InitializeDatabase(TContext context) {
			//bool dbExists;
			//using (new TransactionScope(TransactionScopeOption.Suppress)) {
			//    dbExists = context.Database.Exists();
			//}
			//if (dbExists) {
				//bool createTables = false;
				//if (_tablesToValidate != null && _tablesToValidate.Length > 0) {
				//    //we have some table names to validate
				//    var existingTableNames = new List<string>(context.Database.SqlQuery<string>("SELECT table_name from INFORMATION_SCHEMA.TABLES WHERE table_type = 'base table'"));
				//    createTables = existingTableNames.Intersect(_tablesToValidate, StringComparer.InvariantCultureIgnoreCase).Count() == 0;
				//}
				//else {
				//    //check whether tables are already created
				//    int numberOfTables = 0;
				//    foreach (var t1 in context.Database.SqlQuery<int>("SELECT COUNT(*) from INFORMATION_SCHEMA.TABLES WHERE table_type = 'base table' "))
				//        numberOfTables = t1;

				//    createTables = numberOfTables == 0;
				//}

				//if (createTables) {
					//create all tables
					var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
					System.IO.File.WriteAllText("E:\\cmssql.txt", dbCreationScript);
				    //context.Database.ExecuteSqlCommand(dbCreationScript);
				//    //Seed(context);
				   // context.SaveChanges();
				//}
			//}
			//else {
			//    throw new ApplicationException("No database instance");
			//}
		}
	}
}
