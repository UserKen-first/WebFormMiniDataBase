using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote1.Extensions
{
    public static class StringExtension
    {
        public static Guid ToGuid(this string guidText)
        {
            if (Guid.TryParse(guidText, out Guid tempGuid))
            {
                return tempGuid;
            }
            else
            {
                // return null; // GUID為實質型別，不能回傳null // GUID後+'?'即可使用
                return Guid.Empty;
            }
        } 
    }
}