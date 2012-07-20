using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Data.Service.Helpers;
using System.Data;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;

namespace CMS.Data.Service.ModuleTypes {
	public partial class ModuleTypeRepository:IModuleTypeRepository {

		public PagedList<ModuleTypeDetail> GetModuleTypes(string moduleTypeName,
											int pageIndex,
											int pageSize,
											string sortName,
											string sortOrder) {
			using(CMSContext context=new CMSContext()) {
				IQueryable<ModuleType> query=context.ModuleTypes;
				if(string.IsNullOrEmpty(moduleTypeName)==false) {
					query=query.Where(moduleType => moduleType.ModuleTypeName.StartsWith(moduleTypeName));
				}
				query=query.OrderBy(sortName,(sortOrder=="asc"));
				IQueryable<ModuleTypeDetail> moduleTypes=(from moduleType in query
													select new ModuleTypeDetail {
														ModuleTypeID=moduleType.ModuleTypeID,
														ModuleTypeName=moduleType.ModuleTypeName,
													});
				return new PagedList<ModuleTypeDetail>(moduleTypes,pageIndex,pageSize);
			}
		}

		public IEnumerable<ErrorInfo> SaveModuleType(ModuleType moduleType) {
			using(CMSContext context=new CMSContext()) {
				IEnumerable<ErrorInfo> errors=ValidationHelper.Validate(moduleType);
				if(errors.Any()==false) {
					if(moduleType.ModuleTypeID<=0) {
						context.ModuleTypes.Add(moduleType);
					} else {
						context.Entry(moduleType).State=EntityState.Modified;
					}
					context.SaveChanges();
				}
				return errors;
			}
		}

		public ModuleType FindModuleType(int moduleTypeID) {
			using(CMSContext context=new CMSContext()) {
				return context
					.ModuleTypes
					.Where(m => m.ModuleTypeID==moduleTypeID).FirstOrDefault();
			}
		}

	}
}
