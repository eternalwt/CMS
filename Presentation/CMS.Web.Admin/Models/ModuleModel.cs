using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CMS.Data;
using CMS.Data.Config;
using CMS.Data.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Helpers;

namespace CMS.Web.Admin.Models
{

    public class ModuleModel
    {

        public int ModuleID { get; set; }

        [Required(ErrorMessage = "Module name is required")]
        [StringLength(50, ErrorMessage = "Module name must be under 50 characters")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Module type is required")]
        [Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Module type is required")]
        public int ModuleTypeID { get; set; }

        public string ModuleTypeName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Position is required")]
        public int PositionID { get; set; }

        public string PositionName { get; set; }

        [Required(ErrorMessage = "IsPublish is required")]
        public bool IsPublish { get; set; }

        [DateRange(ErrorMessage = "Invalid PublishUp Date")]
        public DateTime? PublishUp { get; set; }

        public string PublishUpFormat
        {
            get
            {
                return (this.PublishUp.HasValue ? this.PublishUp.Value.ToString("MM/dd/yyyy") : string.Empty);
            }
        }

        [DateRange(ErrorMessage = "Invalid PublishDown Date")]
        public DateTime? PublishDown { get; set; }

        public string PublishDownFormat
        {
            get
            {
                return (this.PublishDown.HasValue ? this.PublishDown.Value.ToString("MM/dd/yyyy") : string.Empty);
            }
        }

        [Required(ErrorMessage = "Sort order is required")]
        [Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Sort order is required")]
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        [Range(ConfigUtil.IDStartRange, int.MaxValue, ErrorMessage = "Access level is required")]
        public int AccessLevelID { get; set; }

        public string AccessLevelName { get; set; }

        public List<int> ModulesInRoles { get; set; }

        public List<PositionDetail> Positions { get; set; }

        public List<AccessLevelDetail> AccessLevels { get; set; }

        public List<RoleDetail> Roles { get; set; }

        public List<ModuleTypeDetail> ModuleTypes { get; set; }

        public Parameter Parameter { get; set; }
    }
}
