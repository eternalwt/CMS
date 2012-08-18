using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Data.Service.Helpers;
using System.Data;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;

namespace CMS.Data.Service.Roles
{
    public partial class RoleRepository : IRoleRepository
    {
        public List<RoleDetail> GetRoles()
        {
            using (CMSContext context = new CMSContext())
            {
                return (from role in context.Roles
                        orderby role.RoleName 
                        select new RoleDetail 
                        {
                           RoleID = role.RoleID,
                             RoleName = role.RoleName 
                        }).ToList();
            }
        }
    }
}
