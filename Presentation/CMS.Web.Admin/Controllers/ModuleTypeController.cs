using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Data.Service.ModuleTypes;
using CMS.Data.Service.Details;
using CMS.Web.Admin.Models;
using CMS.Web.Admin.Helpers;

namespace CMS.Web.Admin.Controllers {
	public class ModuleTypeController:Controller {
		public IModuleTypeRepository _moduleTypeRepository { get; set; }

		public ModuleTypeController()
			: this(new ModuleTypeRepository()) {
		}

		public ModuleTypeController(IModuleTypeRepository moduleTypeRepository) {
			_moduleTypeRepository=moduleTypeRepository;
		}
		//
		// GET: /ModuleType/

		public ActionResult Index() {
			return View();
		}

		[HttpGet]
		public JsonResult Search(string term) {
			List<ModuleTypeDetail> moduleTypes=_moduleTypeRepository.GetModuleTypes(term,1,GlobalConst.AutoCompletePageSize,"ModuleTypeName","asc");
			return Json((from pos in moduleTypes
						 select new AutoCompleteListModel {
							 id=pos.ModuleTypeID,
							 value=pos.ModuleTypeName,
							 label=pos.ModuleTypeName,
						 }).ToArray(),JsonRequestBehavior.AllowGet);
		}
	}
}
