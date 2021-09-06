using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TryCookie
{
    public class Global : System.Web.HttpApplication
    {
        System.IO.FileInfo writer = new System.IO.FileInfo("C:\\Logs\\Log From Global\\log.log"); 
        // 加鎖的這個元件一定要可以初始化
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var userCount = Application["UserCount"] as int?; // 統計數字如果沒有值就初始化
            
            if (!userCount.HasValue) // 假如為初次上線，系統尚未初始化
                userCount = 0;

            userCount += 1;
            Application["UserCount"] = userCount;

            // Session有效的期間，UserCount仍存在
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // 任何頁面打開，都會觸發BeginRequest (除了靜態檔案) 預設 => 不捕捉任何的靜態檔案
            // 只要有人上線，在此捕捉 => 問題會發生 重新整理人數即+1
            // 取得request的所有東西，但此時狀態尚未把驗證所需的東西放入

            //var request = HttpContext.Current.Request;

            //string path = request.Url.PathAndQuery; // 相對路徑

            //if (path.StartsWith("/AdminPage", StringComparison.InvariantCultureIgnoreCase)) => 目前會是空值
            //{
            //    bool isAuth = HttpContext.Current.Request.IsAuthenticated; //檢查使用者是否有登入
            //    var user = HttpContext.Current.User;

            //    if (!isAuth || user == null)
            //    {
            //        response.StatusCode = 403;
            //        response.End();
            //        return;
            //    }
            //    var identity = user.Identity as FormsIdentity; // 系統不會知道是甚麼型別 // 取得校正 => 轉型

            //    if (identity == null)
            //    {
            //        response.StatusCode = 403;
            //        response.End();
            //        return;
            //    }
            //}

                // http://www.google.com/aaa.php?link=123
                // /aaa.php?link=123 => 由根目錄取得網頁及QueryString => 存取後台的頁面

            //var userCount = Application["Users"] as int?;
            //// 假如為初次上線，系統尚未初始化
            //if (!userCount.HasValue)
            //    userCount = 0;

            //userCount += 1;
            //Application["UserCount"] = userCount;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            string path = request.Url.PathAndQuery;

            if (path.StartsWith("/AdminPage", StringComparison.InvariantCultureIgnoreCase))  //忽略大小寫筆對
            {
                // 登入判斷可寫成Class
                bool isAuth = HttpContext.Current.Request.IsAuthenticated; //符合上述路徑的皆檢查使用者是否有登入
                var user = HttpContext.Current.User;

                if (!isAuth || user == null)
                {
                    response.StatusCode = 403;
                    response.End();
                    return;
                }
                var identity = user.Identity as FormsIdentity; // 系統不會知道是甚麼型別 // 取得校正 => 轉型

                if (identity == null)
                {
                    response.StatusCode = 403;
                    response.End();
                    return;
                }
            }
        }
    
    protected void Application_Error(object sender, EventArgs e)
    {
        // 全域的Error做捕捉
        var error = HttpContext.Current.Error;   // 一旦發生錯誤就會在此被捕捉

        string path = "C:\\Logs\\Log From Global\\log.log";

        //Object locker = new object();    // if有多筆錯誤同時發生 => 單筆錯誤寫入流程:鎖定檔案，寫入錯誤，解鎖
        lock (writer)                    // 透過lock 多筆錯誤(多執行序) => 一個一個排隊 (需使用物件)
            {
            using (var streamWriter = writer.AppendText())
            {
                streamWriter.Write(error.Message);                // 缺點 效能低落
            }
            // System.IO.File.WriteAllText(path, error.Message);
        }
    }

    protected void Session_End(object sender, EventArgs e)
    {
            // Session判斷是否有持續登入 (由伺服器判斷) => 伺服器上的Session還在 (無法判斷是否有重複登入)
            
            // 0823問題點: 如何確定線上人數減少?
            
            var userCount = Application["UserCount"] as int?;
            // 假如為初次上線，系統尚未初始化
            if (!userCount.HasValue)
                userCount = 0;

            userCount -= 1;
            Application["UserCount"] = userCount;
        }

    protected void Application_End(object sender, EventArgs e)
    {

    }
}
}

