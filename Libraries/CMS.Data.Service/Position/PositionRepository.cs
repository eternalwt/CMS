using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Data.Service.Helpers;
using System.Data;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Models;
using CMS.Web.FrameWork.Models;

namespace CMS.Data.Service.Positions {
	public partial class PositionRepository:IPositionRepository {

		public PagedList<PositionModel> GetPositions(string positionName,
											int pageIndex,
											int pageSize,
											string sortName,
											string sortOrder) {
			using(CMSContext context=new CMSContext()) {
				IQueryable<Position> query=context.Positions;
				if(string.IsNullOrEmpty(positionName)==false) {
					query=query.Where(position => position.PositionName.StartsWith(positionName));
				}
				query=query.OrderBy(sortName,(sortOrder=="asc"));
				IQueryable<PositionModel> positions=(from position in query
													select new PositionModel {
														PositionID=position.PositionID,
														PositionName=position.PositionName,
													});
				return new PagedList<PositionModel>(positions,pageIndex,pageSize);
			}
		}

		public IEnumerable<ErrorInfo> SavePosition(Position position) {
			using(CMSContext context=new CMSContext()) {
				IEnumerable<ErrorInfo> errors=ValidationHelper.Validate(position);
				if(errors.Any()==false) {
					if(position.PositionID<=0) {
						context.Positions.Add(position);
					} else {
						context.Entry(position).State=EntityState.Modified;
					}
					context.SaveChanges();
				}
				return errors;
			}
		}

		public Position FindPosition(int positionID) {
			using(CMSContext context=new CMSContext()) {
				return context
					.Positions
					.Where(m => m.PositionID==positionID).FirstOrDefault();
			}
		}

	}
}
