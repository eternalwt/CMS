using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CMS.Data {
	public class InstallDefaultData<TContext> : CreateDatabaseIfNotExists<CMSContext> {

		protected override void Seed(CMSContext context) {
			context.ModuleTypes.Add(new ModuleType { ModuleTypeName = "Menu" });
			context.ModuleTypes.Add(new ModuleType { ModuleTypeName = "Login" });
			context.Positions.Add(new Position { PositionName = "Top" });
			context.Positions.Add(new Position { PositionName = "Left" });
			context.Positions.Add(new Position { PositionName = "Bottom" });
			context.Positions.Add(new Position { PositionName = "Right" });
			context.Positions.Add(new Position { PositionName = "Header" });
			context.Positions.Add(new Position { PositionName = "Footer" });
			context.AccessLevels.Add(new AccessLevel { AccessLevelName = "Public" });
			context.AccessLevels.Add(new AccessLevel { AccessLevelName = "Register" });
            context.Roles.Add(new Role { RoleName = "Admin" });
            context.Roles.Add(new Role { RoleName = "Manager" });
            context.Roles.Add(new Role { RoleName = "User" });
		}
	}
}
