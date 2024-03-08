using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Helper
{
    public class ToolsHelper
    {
        public const string REG_GUID = "^([A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12})?$";

        public static bool IsGUID(string guid)
        {
            return guid != null && Regex.IsMatch(guid, REG_GUID);
        }

        /// <summary>
        /// get DisplayAttribute Name of a property of a model
        /// </summary>
        public static string GetDisplayName<TProperty>(Expression<Func<TProperty>> f)
        {
            var exp = f.Body as MemberExpression;
            var property = exp.Expression.Type.GetProperty(exp.Member.Name);
            var attr = property?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            return attr?.GetName() ?? exp.Member.Name;
        }

        #region 用 T1 Properties 建立新 VM
        /// <summary>
        /// 用 T1 Properties 建立新 VM
        /// </summary>
        /// <typeparam name="T1">in Entity</typeparam>
        /// <typeparam name="T2">out New VM</typeparam>
        /// <param name="t1"></param>
        /// <returns></returns>
        public static T2 CreateInstanceUseProperties<T1, T2>(in T1 t1)
        {
            T2 t2 = Activator.CreateInstance<T2>();

            var props = t2.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var p2 in props)
            {
                var value = t1.GetPropertyValue(p2.Name);

                if (value != null)
                {
                    if (value is string value2 && string.IsNullOrEmpty(value2))
                    {
                        continue;
                    }

                    if (p2.CanWrite)
                    {
                        p2.SetValue(t2, value);
                    }
                }
            }

            return t2;
        }
        #endregion
    }
}
