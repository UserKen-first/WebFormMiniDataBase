using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryCookie.AdminPage
{
    public partial class _default : System.Web.UI.Page
    {
        // 用於顯示cookie值
        // Cookie名稱 、 變數名稱 皆須一致 才可讀取
        private const string _loginName = "LoginKey";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuth = HttpContext.Current.Request.IsAuthenticated; //檢查使用者是否有登入
            var user = HttpContext.Current.User;

            if(isAuth && user != null)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity; // 系統不會知道是甚麼型別 // 取得校正

                if (identity == null)
                {
                    this.Literal1.Text = "Not logined";
                    return;
                }

                var userData = identity.Ticket; //建立令牌，丟入userData
                this.Literal1.Text = $"User: {user.Identity.Name}  ID: {identity.Ticket.UserData}";
                //user.IsInRole("")    // 給予字串進行比對 => 比對權限是否正確
            }
            else
            {
                this.Literal1.Text = "Not Logined";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            
            FormsAuthentication.SignOut();
            //var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //cookie.Expires = DateTime.Now.AddSeconds(-5);
            //Response.Cookies.Add(cookie);                     // 刪除cookie，仍需寄一次命令給瀏覽器

            Response.Redirect(Request.RawUrl);
        }
    }
}