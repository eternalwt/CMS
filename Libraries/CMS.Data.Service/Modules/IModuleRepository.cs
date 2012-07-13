using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;
using CMS.Data.Service.Helpers;
 
 

namespace CMS.Data.Service.Modules {
	public interface IModuleRepository {

		PagedList<ModuleDetail> GetModules(string moduleName, int? moduleTypeID, int? positionID, int? accessLevelID, bool? isPublished, int pageIndex, int pageSize, string sortName, string sortOrder);
		IEnumerable<ErrorInfo> SaveModule(Module module);
		Module FindModule(int moduleID);

		List<ModulesInRoles> GetModuleRoles(int moduleID);
		bool DeleteModuleRoles(int moduleID);
		IEnumerable<ErrorInfo> SaveModuleRoles(List<ModulesInRoles> moduleRoles);

		List<ModulesInMenus> GetModuleMenus(int moduleID);
		bool DeleteModuleMenus(int moduleID);
		IEnumerable<ErrorInfo> SaveModuleMenus(List<ModulesInMenus> moduleMenus);

		List<PositionDetail> GetPositions();
		List<AccessLevelDetail> GetAccessLevels();

	}
}
