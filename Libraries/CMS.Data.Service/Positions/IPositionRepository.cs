using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;
using CMS.Data.Service.Helpers;
 
 

namespace CMS.Data.Service.Positions {
	public interface IPositionRepository {

		PagedList<PositionDetail> GetPositions(string positionName,int pageIndex, int pageSize, string sortName, string sortOrder);
		IEnumerable<ErrorInfo> SavePosition(Position position);
		Position FindPosition(int positionID);

	}
}
