using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Web.Admin.Models;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Modules;
using CMS.Data;
using CMS.Web.FrameWork.Models;
using CMS.Data.Service.Details;
using CMS.Data.Service.Helpers;
using CMS.Data.Enums;

namespace CMS.Web.Admin.Controllers {

	public class ModuleController:Controller {

		public IModuleRepository _moduleRepository { get; set; }

		public ModuleController()
			: this(new ModuleRepository()) {
		}

		public ModuleController(IModuleRepository moduleRepository) {
			_moduleRepository=moduleRepository;
		}

		//
		// GET: /Module/

		public ActionResult Index() {
            ModuleModel model = new ModuleModel();
            model.Positions = _moduleRepository.GetPositions();
            model.AccessLevels = _moduleRepository.GetAccessLevels();
			return View();
		}

		//
		// POST: /Module/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			ModuleModel model=new ModuleModel();
			JsonResultInfo result=new JsonResultInfo();
			this.TryUpdateModel(model);
			if(this.ModelState.IsValid) {
				Module module;
				if(model.ModuleID==0)
					module=new Module();
				else
					module=_moduleRepository.FindModule(model.ModuleID);

				module.AccessLevelID=model.AccessLevelID;
				module.IsPublish=model.IsPublish;
				module.ModuleName=model.ModuleName;
				module.ModuleTypeID=model.ModuleTypeID;
				module.PositionID=model.PositionID;
				module.SortOrder=model.SortOrder;
				module.PublishUp=model.PublishUp;
				module.PublishDown=model.PublishDown;

				result.Errors=_moduleRepository.SaveModule(module);

				// Save Module Roles
				if(result.Errors.Any()==false) {
					_moduleRepository.DeleteModuleRoles(module.ModuleID);
                    if (string.IsNullOrEmpty(model.RoleIDs) == false)
                    {
                        string[] arrRoleIDs = model.RoleIDs.Split((",").ToCharArray());
                        int id = 0;
                        List<ModulesInRoles> moduleRoles = new List<ModulesInRoles>();
                        foreach (string roleID in arrRoleIDs)
                        {
                            int.TryParse(roleID, out id);
                            moduleRoles.Add(new ModulesInRoles { ModuleID = module.ModuleID, RoleID = id });
                        }
                        result.Errors = _moduleRepository.SaveModuleRoles(moduleRoles);
                    }
				}

				// Save Module Menus
				if(result.Errors.Any()==false) {
					_moduleRepository.DeleteModuleMenus(module.ModuleID);
                    if (string.IsNullOrEmpty(model.MenuIDs) == false)
                    {
                        string[] arrMenusIDs = model.MenuIDs.Split((",").ToCharArray());
                        int id = 0;
                        List<ModulesInMenus> moduleMenus = new List<ModulesInMenus>();
                        foreach (string menuID in arrMenusIDs)
                        {
                            int.TryParse(menuID, out id);
                            moduleMenus.Add(new ModulesInMenus { ModuleID = module.ModuleID, MenuID = id });
                        }
                        result.Errors = _moduleRepository.SaveModuleMenus(moduleMenus);
                    }
				}

				if(result.Errors.Any()==false) {
					result.ID=module.ModuleID;
				}
			} else {
				result.Errors=ValidationHelper.GetErrorInfo(ModelState);
			}
			return Json(result);
		}

		//
		// POST: /Module/List
		[HttpPost]
		public ActionResult List(FormCollection collection) {
			ModuleModel model=new ModuleModel();
			PagingModel pagingModel=new PagingModel();
			this.TryUpdateModel(model);
			this.TryUpdateModel(pagingModel);
			if(string.IsNullOrEmpty(pagingModel.sortname))
				pagingModel.sortname="ModuleName";
			PagedList<ModuleDetail> modules=_moduleRepository.GetModules(model.ModuleName,
				model.ModuleTypeID,
				model.PositionID,
				model.AccessLevelID,
				model.IsPublish,
				pagingModel.pageindex,
				pagingModel.pagesize,
				pagingModel.sortname,
				pagingModel.sortorder);
			return View(modules);
		}


		//
		// GET: /Module/Delete/5
		[HttpGet]
		public JsonResult Delete(int id) {
			return null;
		}

		[HttpGet]
		public JsonResult Find(int id) {
			Module module=_moduleRepository.FindModule(id);
			ModuleModel model=null;
			if(module!=null) {
				model=new ModuleModel {
					AccessLevelID=module.AccessLevelID,
					IsPublish=module.IsPublish,
					ModuleID=module.ModuleID,
					ModuleName=module.ModuleName,
					ModuleTypeID=module.ModuleTypeID,
					ModuleTypeName=module.ModuleType.ModuleTypeName,
					PositionID=module.PositionID,
					SortOrder=module.SortOrder,
					PositionName=module.Position.PositionName,
					AccessLevelName=module.AccessLevel.AccessLevelName,
				};
			} else {
				model=new ModuleModel{
                    AccessLevelID = (int)AccessLevelEnum.Public,
                    IsPublish = true,
                    SortOrder = 1
            };
			}
			return Json(model,JsonRequestBehavior.AllowGet);
		}

	}
}
