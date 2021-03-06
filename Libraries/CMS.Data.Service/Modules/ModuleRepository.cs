﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Data.Service.Helpers;
using System.Data;
using CMS.Web.FrameWork.Helpers;
using CMS.Data.Service.Details;
using CMS.Web.FrameWork.Models;

namespace CMS.Data.Service.Modules
{
    public partial class ModuleRepository : IModuleRepository
    {

        public PagedList<ModuleDetail> GetModules(string moduleName,
                                            int? moduleTypeID,
                                            int? positionID,
                                            int? accessLevelID,
                                            bool? isPublish,
                                            int pageIndex,
                                            int pageSize,
                                            string sortName,
                                            string sortOrder)
        {
            using (CMSContext context = new CMSContext())
            {
                IQueryable<Module> query = context.Modules;
                if (string.IsNullOrEmpty(moduleName) == false)
                {
                    query = query.Where(m => m.ModuleName.StartsWith(moduleName));
                }
                if ((moduleTypeID ?? 0) > 0)
                {
                    query = query.Where(m => m.ModuleTypeID == moduleTypeID);
                }
                if ((positionID ?? 0) > 0)
                {
                    query = query.Where(m => m.PositionID == positionID);
                }
                if ((accessLevelID ?? 0) > 0)
                {
                    query = query.Where(m => m.AccessLevelID == accessLevelID);
                }
                if (isPublish == true)
                {
                    query = query.Where(m => m.IsPublish == isPublish);
                }
                query = query.OrderBy(sortName, (sortOrder == "asc"));
                IQueryable<ModuleDetail> modules = (from module in query
                                                    select new ModuleDetail
                                                    {
                                                        ModuleID = module.ModuleID,
                                                        ModuleName = module.ModuleName,
                                                        IsPublish = module.IsPublish,
                                                        SortOrder = module.SortOrder,
                                                        AccessLevelName = module.AccessLevel.AccessLevelName,
                                                        ModuleTypeName = module.ModuleType.ModuleTypeName,
                                                        PositionName = module.Position.PositionName
                                                    });
                return new PagedList<ModuleDetail>(modules, pageIndex, pageSize);
            }
        }

        public IEnumerable<ErrorInfo> SaveModule(Module module)
        {
            using (CMSContext context = new CMSContext())
            {
                IEnumerable<ErrorInfo> errors = ValidationHelper.Validate(module);
                if (errors.Any() == false)
                {
                    if (module.ModuleID <= 0)
                    {
                        context.Modules.Add(module);
                    }
                    else
                    {
                        context.Entry(module).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
                return errors;
            }
        }

        public Module FindModule(int moduleID)
        {
            using (CMSContext context = new CMSContext())
            {
                return context
                    .Modules
                    .Include("Position")
                    .Include("AccessLevel")
                    .Include("ModuleType")
                    .Include("ModulesInRoles")
                    .Where(m => m.ModuleID == moduleID).FirstOrDefault();
            }
        }

        public List<ModulesInRoles> GetModuleRoles(int moduleID)
        {
            using (CMSContext context = new CMSContext())
            {
                return context.ModulesInRoles.Where(moduleRole => moduleRole.ModuleID == moduleID).ToList();
            }
        }

        public bool DeleteModuleRoles(int moduleID)
        {
            using (CMSContext context = new CMSContext())
            {
                List<ModulesInRoles> ModuleRoles = context.ModulesInRoles.Where(moduleposition => moduleposition.ModuleID == moduleID).ToList();
                foreach (var moduleposition in ModuleRoles)
                {
                    context.ModulesInRoles.Remove(moduleposition);
                }
                return context.SaveChanges() > 0;
            }
        }

        public IEnumerable<ErrorInfo> SaveModuleRoles(List<ModulesInRoles> ModuleRoles)
        {
            using (CMSContext context = new CMSContext())
            {
                List<ErrorInfo> errors = new List<ErrorInfo>();
                foreach (var moduleposition in ModuleRoles)
                {
                    errors.AddRange(ValidationHelper.Validate(moduleposition));
                    if (errors.Any() == false)
                        context.ModulesInRoles.Add(moduleposition);
                }
                if (errors.Any() == false)
                    context.SaveChanges();
                return errors;
            }
        }

        public List<ModulesInMenus> GetModuleMenus(int moduleID)
        {
            using (CMSContext context = new CMSContext())
            {
                return context.ModulesInMenus.Where(moduleMenu => moduleMenu.ModuleID == moduleID).ToList();
            }
        }

        public bool DeleteModuleMenus(int moduleID)
        {
            using (CMSContext context = new CMSContext())
            {
                List<ModulesInMenus> moduleMenus = context.ModulesInMenus.Where(moduleMenu => moduleMenu.ModuleID == moduleID).ToList();
                foreach (var moduleMenu in moduleMenus)
                {
                    context.ModulesInMenus.Remove(moduleMenu);
                }
                return context.SaveChanges() > 0;
            }
        }

        public IEnumerable<ErrorInfo> SaveModuleMenus(List<ModulesInMenus> moduleMenus)
        {
            using (CMSContext context = new CMSContext())
            {
                List<ErrorInfo> errors = new List<ErrorInfo>();
                foreach (var moduleMenu in moduleMenus)
                {
                    errors.AddRange(ValidationHelper.Validate(moduleMenu));
                    if (errors.Any() == false)
                        context.ModulesInMenus.Add(moduleMenu);
                }
                if (errors.Any() == false)
                    context.SaveChanges();
                return errors;
            }
        }

        public List<AccessLevelDetail> GetAccessLevels()
        {
            using (CMSContext context = new CMSContext())
            {
                return (from accessLevel in context.AccessLevels
                        orderby accessLevel.AccessLevelName
                        select new AccessLevelDetail
                        {
                            AccessLevelID = accessLevel.AccessLevelID,
                            AccessLevelName = accessLevel.AccessLevelName
                        }).ToList();
            }
        }

        public List<PositionDetail> GetPositions()
        {
            using (CMSContext context = new CMSContext())
            {
                return (from position in context.Positions 
                        orderby position.PositionName 
                        select new PositionDetail
                        {
                            PositionID = position.PositionID,
                            PositionName = position.PositionName 
                        }).ToList();
            }
        }
    }
}
