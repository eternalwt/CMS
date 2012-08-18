using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;
using CMS.Data.Service.Helpers;
using CMS.Data.Service.Details;

namespace CMS.Data.Service.Roles {
	public interface IRoleRepository {
		List<RoleDetail> GetRoles();
	}
}
