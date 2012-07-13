using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Data.Service.Positions;
using CMS.Data.Service.Details;
using CMS.Web.Admin.Models;
using CMS.Web.Admin.Helpers;

namespace CMS.Web.Admin.Controllers {
	public class PositionController:Controller {
		public IPositionRepository _positionRepository { get; set; }

		public PositionController()
			: this(new PositionRepository()) {
		}

		public PositionController(IPositionRepository positionRepository) {
			_positionRepository=positionRepository;
		}
		//
		// GET: /Position/

		public ActionResult Index() {
			return View();
		}

		[HttpGet]
		public JsonResult Search(string term) {
			List<PositionDetail> positions=_positionRepository.GetPositions(term,1,GlobalConst.AutoCompletePageSize,"PositionName","asc");
			return Json((from pos in positions
						 select new AutoCompleteListModel {
							 id=pos.PositionID,
							 value=pos.PositionName,
							 label=pos.PositionName,
						 }).ToArray(),JsonRequestBehavior.AllowGet);
		}
	}
}
