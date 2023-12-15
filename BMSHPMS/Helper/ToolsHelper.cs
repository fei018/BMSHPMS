using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System;
using System.Text.RegularExpressions;
using System.Linq;

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
    }
}
