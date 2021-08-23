using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.Auth
{
    public class UserInfoModel
    {
        public Guid ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //public Guid UserGUID
        //{
        //    get // get本身也可為一個方法
        //    {
        //        if (Guid.TryParse(this.ID, out Guid tempGuid))
        //        {
        //            return tempGuid;
        //        }
        //        else
        //        {
        //            // return null; // GUID為實質型別，不能回傳null // GUID後+'?'即可使用
        //            return Guid.Empty;
        //        }
        //    }
        //}
    }
}

