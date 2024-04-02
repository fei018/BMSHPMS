namespace BMSHPMS.Helper
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 所有屬性類型為 string 類型 invoke Trim()
        /// </summary>
        /// <param name="obj"></param>
        public static void TrimAsString(this object obj)
        {
            var props = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    var str = prop.GetValue(obj) as string;

                    if (!string.IsNullOrEmpty(str))
                    {
                        prop.SetValue(obj,str.Trim());
                    }
                }
            }
        }
    }
}
