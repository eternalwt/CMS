using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Data.Service.ModuleTypes;
using CMS.Data.Service.Details;
using CMS.Web.Admin.Models;
using CMS.Web.Admin.Helpers;
using CMS.Data;
using System.IO;
using CMS.Web.FrameWork.Helpers;

namespace CMS.Web.Admin.Controllers
{
    public class ModuleTypeController : Controller
    {
        public IModuleTypeRepository _moduleTypeRepository { get; set; }

        public ModuleTypeController()
            : this(new ModuleTypeRepository())
        {
        }

        public ModuleTypeController(IModuleTypeRepository moduleTypeRepository)
        {
            _moduleTypeRepository = moduleTypeRepository;
        }

        //
        // GET: /ModuleType/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Search(string term)
        {
            List<ModuleTypeDetail> moduleTypes = _moduleTypeRepository.GetModuleTypes(term, 1, GlobalConst.AutoCompletePageSize, "ModuleTypeName", "asc");
            return Json((from pos in moduleTypes
                         select new AutoCompleteListModel
                         {
                             id = pos.ModuleTypeID,
                             value = pos.ModuleTypeName,
                             label = pos.ModuleTypeName,
                         }).ToArray(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetParameters(int id)
        {
            ModuleType moduleType = _moduleTypeRepository.FindModuleType(id);
            string filePath;
            string xml;
            Parameter parameter = null;
            if (moduleType != null)
            {
                filePath = Path.Combine(Server.MapPath("~/"), "Modules", moduleType.ModuleTypeName, "parameters.xml");
                if (System.IO.File.Exists(filePath))
                {
                    xml = System.IO.File.ReadAllText(filePath);
                    parameter = ParameterHelper.GetParameters(xml);
                }
            }
            return Json(parameter, JsonRequestBehavior.AllowGet);
        }


    }
}
