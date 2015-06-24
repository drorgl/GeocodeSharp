using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeocodeSharp
{
    internal class EnumUtils
    {
        public static string GetValue(Enum value)
        {
            var enumMemberAttr =  value.GetType()
                .GetMember(value.ToString())[0].GetCustomAttributes(typeof(EnumMemberAttribute), true)
                .Cast<EnumMemberAttribute>()
                .FirstOrDefault();
            if (enumMemberAttr != null)
            {
                return enumMemberAttr.Value;
            }

            return value.ToString();
        }
    }
}
