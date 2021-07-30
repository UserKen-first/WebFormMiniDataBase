using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    // 類別要盡量分門別類，DBSource為資料庫相關，Auth負責處理登入元件
    public class AuthManager
    {
        public static bool IsLogined()   //功能為單純檢查是否有功能
        {
            /// <summary>check is logined</summary> 
            /// <returns></returns>
            if (HttpContext.Current.Session["UserLoginInfo"] == null)   //封裝此行
                return false;
            else
                return true;
        }
    }
}
