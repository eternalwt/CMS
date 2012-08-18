using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using CMS.Data;
using CMS.Data.Map;
using System.Data;


namespace CMS.Data {
	/// <summary>
	/// Object context
	/// </summary>
	public class CMSContext : DbContext, IDisposable {

		public DbSet<Module> Modules { get; set; }

		public DbSet<ModulesInRoles> ModulesInRoles { get; set; }

		public DbSet<ModulesInMenus> ModulesInMenus { get; set; }

		public DbSet<ModuleType> ModuleTypes { get; set; }

		public DbSet<Position> Positions { get; set; }

		public DbSet<AccessLevel> AccessLevels { get; set; }

        public DbSet<Role> Roles { get; set; }

		//public CMSContext()
		//:base(DataSettingsHelper.ConnectionString) {
		//}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			//dynamically load all configuration
			System.Type configType = typeof(ModuleMap);   //any of your configuration classes here
			var typesToRegister = System.Reflection.Assembly.GetAssembly(configType).GetTypes()
			.Where(type => !String.IsNullOrEmpty(type.Namespace))
			.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
			foreach (var type in typesToRegister) {
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}
			modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges() {
			foreach (DbEntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>()) {
				if (entry.State == EntityState.Added) {
					entry.Entity.CreatedBy = 1;
					entry.Entity.CreatedDate = DateTime.Now;
				}
				entry.Entity.LastUpdatedBy = 1;
				entry.Entity.LastUpdatedDate = DateTime.Now;
			}
			return base.SaveChanges();
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
		}

	}

}
