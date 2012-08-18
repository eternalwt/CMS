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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using CMS.Data.Service.ModuleTypes;
using CMS.Data.Service.Roles;

namespace CMS.Web.Admin.Controllers
{

    public class ModuleController : Controller
    {

        public IModuleRepository _moduleRepository { get; set; }
        public IRoleRepository _roleRepository { get; set; }
        public IModuleTypeRepository _moduleTypeRepository { get; set; }

        public ModuleController()
            : this(new ModuleRepository(), new ModuleTypeRepository(), new RoleRepository())
        {
        }

        public ModuleController(IModuleRepository moduleRepository, IModuleTypeRepository moduleTypeRepository, IRoleRepository roleRepository)
        {
            _moduleRepository = moduleRepository;
            _moduleTypeRepository = moduleTypeRepository;
            _roleRepository = roleRepository;
        }

        //
        // GET: /Module/
        public ActionResult Index()
        {
            ModuleModel model = new ModuleModel();
            model.Positions = _moduleRepository.GetPositions();
            model.AccessLevels = _moduleRepository.GetAccessLevels();
            model.Roles = _roleRepository.GetRoles();
            model.ModuleTypes = _moduleTypeRepository.GetModuleTypes();
            return View(model);
        }

        //
        // POST: /Module/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ModuleModel model = new ModuleModel();
            JsonResultInfo result = new JsonResultInfo();
            this.TryUpdateModel(model);
            if (this.ModelState.IsValid)
            {
                Module module;
                if (model.ModuleID == 0)
                    module = new Module();
                else
                    module = _moduleRepository.FindModule(model.ModuleID);

                module.AccessLevelID = model.AccessLevelID;
                module.IsPublish = model.IsPublish;
                module.ModuleName = model.ModuleName;
                module.ModuleTypeID = model.ModuleTypeID;
                module.PositionID = model.PositionID;
                module.SortOrder = model.SortOrder;
                module.PublishUp = model.PublishUp;
                module.PublishDown = model.PublishDown;

                ModuleType moduleType = _moduleTypeRepository.FindModuleType(module.ModuleTypeID);
                if (moduleType != null)
                {
                    string filePath = Path.Combine(Server.MapPath("~/"), "Modules", moduleType.ModuleTypeName, "parameters.xml");
                    if (System.IO.File.Exists(filePath))
                    {
                        Parameter p = ParameterHelper.FillParameterValues(System.IO.File.ReadAllText(filePath), collection);
                        MemoryStream stream = new MemoryStream();
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Parameter));
                        ser.WriteObject(stream, p);
                        stream.Position = 0;
                        StreamReader sr = new StreamReader(stream);
                        module.Parameters = sr.ReadToEnd();
                    }
                }

                result.Errors = _moduleRepository.SaveModule(module);

                // Save Module Roles
                if (result.Errors.Any() == false)
                {
                    _moduleRepository.DeleteModuleRoles(module.ModuleID);
                    if (string.IsNullOrEmpty(collection["RolesIDs"]) == false)
                    {
                        string[] arrRoleIDs = collection["RolesIDs"].Split((",").ToCharArray());
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
                if (result.Errors.Any() == false)
                {
                    _moduleRepository.DeleteModuleMenus(module.ModuleID);
                    if (string.IsNullOrEmpty(collection["MenuIDs"]) == false)
                    {
                        string[] arrMenusIDs = collection["MenuIDs"].Split((",").ToCharArray());
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

                if (result.Errors.Any() == false)
                {
                    result.ID = module.ModuleID;
                }
            }
            else
            {
                result.Errors = ValidationHelper.GetErrorInfo(ModelState);
            }
            return Json(result);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFiles(FormCollection collection)
        {
            ParamterFileModel model = new ParamterFileModel();
            JsonResultInfo result = new JsonResultInfo();
            this.TryUpdateModel(model);
            if (this.ModelState.IsValid)
            {
                List<UploadFileModel> uploadFiles = null;
                int index = 0;
                string fileName = string.Empty;
                for (index = 0; index < Request.Files.Count; index++)
                {
                    fileName = Request.Files[index].FileName;
                    if (string.IsNullOrEmpty(fileName) == false)
                    {
                        uploadFiles.Add(UploadFileHelper.Upload(Request.Files[index], "ParameterFileUploadPath", Request.Files[index].FileName));
                    }
                }
                return Json(new { Error = string.Empty, Files = uploadFiles });
            }
            else
            {
                result.Errors = ValidationHelper.GetErrorInfo(ModelState);
            }
            return Json(result);
        }


        //
        // POST: /Module/List
        [HttpPost]
        public ActionResult List(FormCollection collection)
        {
            ModuleModel model = new ModuleModel();
            PagingModel pagingModel = new PagingModel();
            this.TryUpdateModel(model);
            this.TryUpdateModel(pagingModel);
            if (string.IsNullOrEmpty(pagingModel.sortname))
                pagingModel.sortname = "ModuleName";
            PagedList<ModuleDetail> modules = _moduleRepository.GetModules(model.ModuleName,
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
        public JsonResult Delete(int id)
        {
            return null;
        }

        [HttpGet]
        public JsonResult Find(int id)
        {
            Module module = _moduleRepository.FindModule(id);
            ModuleModel model = null;
            if (module != null)
            {
                model = new ModuleModel
                {
                    AccessLevelID = module.AccessLevelID,
                    IsPublish = module.IsPublish,
                    ModuleID = module.ModuleID,
                    ModuleName = module.ModuleName,
                    ModuleTypeID = module.ModuleTypeID,
                    ModuleTypeName = module.ModuleType.ModuleTypeName,
                    PositionID = module.PositionID,
                    SortOrder = module.SortOrder,
                    PositionName = module.Position.PositionName,
                    AccessLevelName = module.AccessLevel.AccessLevelName,
                    PublishDown = module.PublishDown,
                    PublishUp = module.PublishUp,
                    ModulesInRoles = module.ModulesInRoles.Select(moduleRole => moduleRole.RoleID).ToList()
                };
                if (string.IsNullOrEmpty(module.Parameters) == false)
                {
                    string filePath = Path.Combine(Server.MapPath("~/"), "Modules", model.ModuleTypeName, "parameters.xml");
                    if (System.IO.File.Exists(filePath))
                    {
                        model.Parameter = ParameterHelper.CheckParamters(System.IO.File.ReadAllText(filePath), module.Parameters);
                    }
                }
            }
            else
            {
                model = new ModuleModel
                {
                    AccessLevelID = (int)AccessLevelEnum.Public,
                    IsPublish = true,
                    SortOrder = 1,
                    Parameter = new Parameter()
                };
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}
