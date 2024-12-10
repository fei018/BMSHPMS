using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Opt_DharmaServiceVMs
{
    public partial class Opt_DharmaServiceVM : BaseCRUDVM<Opt_DharmaService>
    {

        public Opt_DharmaServiceVM()
        {
        }

        protected override void InitVM()
        {
            FrameworkRoleIdSelectListItems = Get_DSRolesSelectListItems(Entity.ID);
        }

        public override void DoAdd()
        {
            if (DC.Set<Opt_DharmaService>().Any(d => d.SerialCode == Entity.SerialCode))
            {
                Wtm.MSD.AddModelError("SerialCode", $"{ToolsHelper.GetDisplayName(() => Entity.SerialCode)} 已存在.");
                return;
            }

            if (DC.Set<Opt_DharmaService>().Any(d => d.ServiceName == Entity.ServiceName))
            {
                Wtm.MSD.AddModelError("ServiceName", $"{ToolsHelper.GetDisplayName(() => Entity.ServiceName)} 已存在.");
                return;
            }

            using var transaction = DC.BeginTransaction();

            try
            {
                Entity.CreateBy = LoginUserInfo.Name;
                Entity.UpdateBy = LoginUserInfo.Name;
                Entity.CreateTime = DateTime.Now;
                Entity.UpdateTime = DateTime.Now;

                DC.Set<Opt_DharmaService>().Add(Entity);
                DC.SaveChanges();

                UpdateDServiceRole(Entity.ID, FrameworkRoleSelectedIds);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MSD.AddModelError("",ex.Message);
                return;
            }
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            using var transaction = DC.BeginTransaction();

            try
            {
                var old = DC.Set<Opt_DharmaService>().Find(Entity.ID);

                if (!string.IsNullOrEmpty(Entity.ServiceName)) old.ServiceName = Entity.ServiceName;
                if (!string.IsNullOrEmpty(Entity.SerialCode)) old.SerialCode = Entity.SerialCode;
                if (!string.IsNullOrEmpty(Entity.ServiceDateDescription)) old.ServiceDateDescription = Entity.ServiceDateDescription;
                if (!string.IsNullOrEmpty(Entity.ServiceOrganizer)) old.ServiceOrganizer = Entity.ServiceOrganizer;

                old.Enable = Entity.Enable;

                DC.UpdateEntity(old);
                DC.SaveChanges();

                UpdateDServiceRole(Entity.ID, FrameworkRoleSelectedIds);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MSD.AddModelError("", ex.Message);
                return;
            }
        }

        public override void DoDelete()
        {
            using var transaction = DC.BeginTransaction();
            try
            {
                DC.Set<Opt_DharmaService>().Remove(Entity);
                DC.SaveChanges();

                DeleteDServiceRole(Entity.ID);

                transaction.Commit();
            }
            catch (Exception ex )
            {
                transaction.Rollback();
                MSD.AddModelError("", ex.Message);
                return;
            }
        }

        #region 法會關聯用戶角色

        [Display(Name = "法會關聯用戶角色")]
        public string[] FrameworkRoleSelectedIds { get; set; }

        public List<ComboSelectListItem> FrameworkRoleIdSelectListItems { get; set; }

        /// <summary>
        /// 从数据库获取法会的关联用户角色checkbox 選擇項
        /// </summary>
        /// <returns></returns>
        private List<ComboSelectListItem> Get_DSRolesSelectListItems(Guid dsId)
        {
            var selectList = new List<ComboSelectListItem>();

            var allFrameworkRole = DC.Set<FrameworkRole>().OrderBy(x => x.RoleCode).ToList();

            var dsRoles = DC.Set<Opt_DServiceRole>().CheckID(dsId, x => x.DSId).ToList();

            if (dsRoles == null || dsRoles.Count <= 0)
            {
                foreach (var frameworkRole in allFrameworkRole)
                {
                    selectList.Add(new ComboSelectListItem { Text = frameworkRole.RoleName, Value = frameworkRole.ID.ToString(), Selected = false });
                }

                return selectList;
            }

            foreach (var frameworkRole in allFrameworkRole)
            {
                var selectItem = new ComboSelectListItem { Text = frameworkRole.RoleName, Value = frameworkRole.ID.ToString(), Selected = false };

                if (dsRoles.Any(x => x.FrameworkRoleId == frameworkRole.ID))
                {
                    selectItem.Selected = true;
                }

                selectList.Add(selectItem);
            }

            return selectList;
        }

        /// <summary>
        /// 更新 法會關聯用戶角色到數據庫
        /// </summary>
        /// <param name="dsId"></param>
        /// <param name="frameworkRoleSelectedIds"></param>
        public void UpdateDServiceRole(Guid dsId, string[] frameworkRoleSelectedIds)
        {
            var dsRoles = DC.Set<Opt_DServiceRole>().CheckID(dsId, x => x.DSId).ToList();

            //if (dsRoles != null && dsRoles.Count > 0)
            //{
            //    DC.Set<Opt_DServiceRole>().RemoveRange(dsRoles);
            //    DC.SaveChanges();
            //}

            //foreach (var roleId in frameworkRoleSelectedIds)
            //{
            //    var dsRole = new Opt_DServiceRole { DSId = dsId, FrameworkRoleId = Guid.Parse(roleId) };

            //    DC.AddEntity(dsRole);
            //}

            // Opt_DServiceRole 數據庫裏沒有，則直接添加
            if (dsRoles == null || dsRoles.Count <= 0)
            {
                foreach (var selectRoleId in frameworkRoleSelectedIds)
                {
                    var dsRole = new Opt_DServiceRole { DSId = dsId, FrameworkRoleId = Guid.Parse(selectRoleId) };

                    DC.AddEntity(dsRole);
                }

                DC.SaveChanges();
                return;
            }

            // Opt_DServiceRole 數據庫裏存在選擇的 FrameworkRoleId，則跳過繼續循環
            // 不存在選擇的 FrameworkRoleId，則添加進數據庫
            foreach (var selectRoleId in frameworkRoleSelectedIds)
            {
                if (dsRoles.Any(x => x.FrameworkRoleId.ToString().ToLower() == selectRoleId.ToLower()))
                {
                    continue;
                }
                else
                {
                    var dsRole = new Opt_DServiceRole { DSId = dsId, FrameworkRoleId = Guid.Parse(selectRoleId) };

                    DC.AddEntity(dsRole);
                }
            }

            // Opt_DServiceRole 中有 沒有選擇的 FrameworkRoleId，則刪除
            foreach (var dsRole in dsRoles)
            {
                if (!frameworkRoleSelectedIds.Any(x=>x.ToLower() == dsRole.FrameworkRoleId.ToString().ToLower()))
                {
                    DC.DeleteEntity(dsRole);
                }
            }

            DC.SaveChanges();
        }

        public void DeleteDServiceRole(Guid dsId)
        {
            var dsRoles = DC.Set<Opt_DServiceRole>().CheckID(dsId, x => x.DSId).ToList();

            DC.Set<Opt_DServiceRole>().RemoveRange(dsRoles);

            DC.SaveChanges();
        }
        #endregion

        #region Details
        [Display(Name = "法會關聯用戶角色")]
        public string DSRolesName { get; set; }

        public string GetDServiceRolesNames(Guid dsId)
        {
            var list = Get_DSRolesSelectListItems(dsId).Where(x => x.Selected).Select(x => x.Text).ToList();

            return string.Join(", ", list);
        }

        public void InitDetails()
        {
            DSRolesName = GetDServiceRolesNames(Entity.ID);
        }
        #endregion
    }
}
