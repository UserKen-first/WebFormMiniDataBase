using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote1.Models
{
    public class AccountnigNoteViewModel  //任務是乘載資料
    {
        public string ID { get; set; }
        public string Caption { get; set; }
        public int Amount { get; set; }
        public string ActType { get; set; }
        public string CreatDate { get; set; }
        public string Body { get; set; }
    }
}