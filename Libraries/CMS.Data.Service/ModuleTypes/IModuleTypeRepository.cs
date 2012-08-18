using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;
using CMS.Data.Service.Helpers;
 
 

namespace CMS.Data.Service.ModuleTypes {
	public interface IModuleTypeRepository {

		PagedList<ModuleTypeDetail> GetModuleTypes(string moduleTypeName,int pageIndex, int pageSize, string sortName, string sortOrder);
        List<ModuleTypeDetail> GetModuleTypes();
		IEnumerable<ErrorInfo> SaveModuleType(ModuleType moduleType);
		ModuleType FindModuleType(int moduleTypeID);

	}
}
