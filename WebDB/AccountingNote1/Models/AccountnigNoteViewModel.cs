using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote1.Models
{
    public class AccountnigNoteViewModel  //任務是乘載資料
    {
        // 0811 問題點 : 運用Model的這個方式
        public string ID { get; set; }
        public string Caption { get; set; }
        public int Amount { get; set; }
        public string ActType { get; set; }
        public string CreateDate { get; set; }
        public string Body { get; set; }
    }
}