using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote1.Models
{
    public class AccountnigNoteViewModel  //任務是乘載資料的容器
    {
        public string ID { get; set; } //透過這些屬性再寫入這些值
        public string Caption { get; set; }
        public int Amount { get; set; }
        public string ActType { get; set; }
        public string CreateDate { get; set; }
        public string Body { get; set; }
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