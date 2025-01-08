using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels.Common
{
    public class DSRegisterHelper
    {
        #region 根据用户角色显示法会列表
        /// <summary>
        /// 根据用户角色显示法会列表
        /// </summary>
        /// <param name="wtm"></param>
        /// <returns></returns>
        public List<Opt_DharmaService> GetDisplayDharmaServiceList(WTMContext wtm)
        {
            List<Opt_DharmaService> dsList = new List<Opt_DharmaService>();

            // 獲取登錄用戶的角色Ids
            var loginRoleIds = wtm.LoginUserInfo.Roles.Select(x => x.ID).ToList().ConvertAll(x => Convert.ToString(x));

            // 根據登錄用戶角色Ids 獲取 Opt_DServiceRole 表裏的法會Ids
            var dsIds = wtm.DC.Set<Opt_DServiceRole>().CheckIDs(loginRoleIds, x => x.FrameworkRoleId)
                                .Select(x => x.DSId)
                                .Distinct()
                                .ToList()
                                .ConvertAll(x => Convert.ToString(x));

            if (dsIds != null && dsIds.Count > 0)
            {
                // 獲取要顯示的法會
                dsList = wtm.DC.Set<Opt_DharmaService>()
                                    .Where(x => x.Enable)
                                    .CheckIDs(dsIds, x => x.ID)
                                    .OrderBy(x => x.SerialCode)
                                    .ToList();
            }

            return dsList;
        }
        #endregion

    }
}
