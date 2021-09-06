using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;

namespace TryCookie
{
    public partial class Login : System.Web.UI.Page
        
    {
        private const string _loginName = "LoginKey"; // Cookie名
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if ("Admin" == this.txtAccount.Text && "12345" == this.txtPWD.Text)
            {
                string account = "will";
                string userID = "S00011";
                string[] roles = { "Admin" };
                bool isPersistence = false;

                FormsAuthentication.SetAuthCookie(account, isPersistence);   // 誰在使用, 不持久存在
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket( //校正本身
                    1,
                    account,
                    DateTime.Now,
                    DateTime.Now.AddHours(12),
                    isPersistence,
                    userID
                    );

                FormsIdentity identity = new FormsIdentity(ticket); // 記憶體的變數 FormsIdentity-> 校正
                HttpCookie cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,            // Cookie的Nmae修改為FormsCookieName
                    FormsAuthentication.Encrypt(ticket)             // Cookie本身只能放文字 => 序列化
                    );
                cookie.HttpOnly = true;

                GenericPrincipal gp = new GenericPrincipal(identity, roles); // 使用者設定為做好的校正
                HttpContext.Current.Response.Cookies.Add(cookie);


                Response.Redirect("AdminPage/default.aspx");
            }
        }
    }
}






//HttpCookie cookie = new HttpCookie(_loginName);   // New 一個 Cookie
//cookie["Account"] = "Admin";                      // Cookie 內可存放多個值
//cookie["Test"] = "Test";
//cookie.Expires = DateTime.Now.AddMinutes(5);    // Cookie 5分鐘後過期 // 超過時間瀏覽器自砍 // HttpOnly:false
//cookie.Expires = DateTime.Now;                    // Cookie馬上過期 => null // 如無設定 => 預設為一個月或瀏覽器關閉
//Response.Cookies.Add(cookie);
//cookie.HttpOnly = true;                       // 增加保護性
//Session["Test"] = "test";                       // HttpOnly:true  => 不會被Browser端的JS使用